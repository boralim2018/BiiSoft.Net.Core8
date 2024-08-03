using Abp.Application.Services.Dto;
using Abp.Authorization;
using BiiSoft.Authorization;
using BiiSoft.BFiles;
using BiiSoft.Locations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Abp.Extensions;
using BiiSoft.Authorization.Users;
using BiiSoft.Columns;
using OfficeOpenXml;
using BiiSoft.Extensions;
using BiiSoft.FileStorages;
using BiiSoft.Folders;
using Abp.Domain.Uow;
using System.Transactions;
using BiiSoft.Entities;

namespace BiiSoft.Locations
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class LocationAppService : BiiSoftAppServiceBase, ILocationAppService
    {
        private readonly ILocationManager _locationManager;
        private readonly IBiiSoftRepository<Location, Guid> _locationRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IAppFolders _appFolders;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public LocationAppService(
            IUnitOfWorkManager unitOfWorkManager,
            IFileStorageManager fileStorageManager,
            IAppFolders appFolders,
            ILocationManager locationManager,
            IBiiSoftRepository<Location, Guid> locationRepository,
            IBiiSoftRepository<User, long> userRepository)
        {
            _locationManager=locationManager;
            _locationRepository=locationRepository;
            _userRepository=userRepository;
            _fileStorageManager=fileStorageManager;
            _appFolders=appFolders;
            _unitOfWorkManager=unitOfWorkManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Create)]
        public async Task<Guid> Create(CreateUpdateLocationInputDto input)
        {
            var entity = MapEntity<Location, Guid>(input);

            await _locationManager.InsertAsync(entity);
            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            CheckErrors(await _locationManager.DeleteAsync(input.Id));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Disable)]
        public async Task Disable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _locationManager.DisableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Enable)]
        public async Task Enable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _locationManager.EnableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Find_Locations)]
        public async Task<PagedResultDto<FindLocationDto>> Find(PageLocationInputDto input)
        {
            var query = from l in _locationRepository.GetAll()
                                .AsNoTracking()
                                .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                                .WhereIf(input.Creators != null && input.Creators.Ids != null && input.Creators.Ids.Any(), s =>
                                    (input.Creators.Exclude && (!s.CreatorUserId.HasValue || !input.Creators.Ids.Contains(s.CreatorUserId))) ||
                                    (!input.Creators.Exclude && input.Creators.Ids.Contains(s.CreatorUserId)))
                                .WhereIf(input.Modifiers != null && input.Modifiers.Ids != null && input.Modifiers.Ids.Any(), s =>
                                    (input.Modifiers.Exclude && (!s.LastModifierUserId.HasValue || !input.Modifiers.Ids.Contains(s.LastModifierUserId))) ||
                                    (!input.Modifiers.Exclude && input.Modifiers.Ids.Contains(s.LastModifierUserId)))
                                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s =>
                                    s.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                                    s.DisplayName.ToLower().Contains(input.Keyword.ToLower()))
                        select new FindLocationDto
                        {
                            Id = l.Id,
                            Name = l.Name,
                            DisplayName = l.DisplayName,
                            IsActive = l.IsActive,
                        };

            var totalCount = await query.CountAsync();
            var items = new List<FindLocationDto>();
            if (totalCount > 0)
            {
                query = query.OrderBy(input.GetOrdering());
                if (input.UsePagination) query = query.PageBy(input);
                items = await query.ToListAsync();
            }

            return new PagedResultDto<FindLocationDto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_View, PermissionNames.Pages_Setup_Locations_Edit)]
        public async Task<LocationDetailDto> GetDetail(EntityDto<Guid> input)
        {
            var query = from l in _locationRepository.GetAll()
                                .AsNoTracking()
                                .Where(s => s.Id == input.Id)
                        join u in _userRepository.GetAll().AsNoTracking()
                        on l.CreatorUserId equals u.Id
                        join m in _userRepository.GetAll().AsNoTracking()
                        on l.LastModifierUserId equals m.Id
                        into modify
                        from m in modify.DefaultIfEmpty()
                        select new LocationDetailDto
                        {
                            Id = l.Id,
                            No = l.No,
                            Name = l.Name,
                            DisplayName = l.DisplayName,
                            CannotDelete = l.CannotDelete,
                            CannotEdit = l.CannotEdit,
                            IsActive = l.IsActive,
                            Latitude = l.Latitude,
                            Longitude = l.Longitude,
                            CreationTime = l.CreationTime,
                            CreatorUserId = l.CreatorUserId,
                            CreatorUserName = u.UserName,
                            LastModificationTime = l.LastModificationTime,
                            LastModifierUserId = l.LastModifierUserId,
                            LastModifierUserName = m == null ? "" : m.UserName
                        };

            var result = await query.FirstOrDefaultAsync();
            if (result == null) throw new UserFriendlyException(L("RecordNotFound"));

            var record = await _locationRepository.GetAll()
                               .AsNoTracking()
                               .OrderBy(s => s.No)
                               .GroupBy(s => 1)
                               .Select(s => new
                               {
                                   First = s.Where(r => r.No < result.No).Select(n => new { n.No, n.Id }).OrderBy(o => o.No).FirstOrDefault(),
                                   Pervious = s.Where(r => r.No < result.No).Select(n => new { n.No, n.Id }).OrderByDescending(o => o.No).FirstOrDefault(),
                                   Next = s.Where(r => r.No > result.No).Select(n => new { n.No, n.Id }).OrderBy(o => o.No).FirstOrDefault(),
                                   Last = s.Where(r => r.No > result.No).Select(n => new { n.No, n.Id }).OrderByDescending(o => o.No).FirstOrDefault(),
                               })
                               .FirstOrDefaultAsync();

            if (record.First != null) result.FirstId = record.First.Id;
            if (record.Pervious != null) result.PreviousId = record.Pervious.Id;
            if (record.Next != null) result.NextId = record.Next.Id;
            if (record.Last != null) result.LastId = record.Last.Id;

            return result;
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_List)]
        public async Task<PagedResultDto<LocationListDto>> GetList(PageLocationInputDto input)
        {
            return await GetListHelper(input);
        }

        private async Task<PagedResultDto<LocationListDto>> GetListHelper(PageLocationInputDto input)
        {
            var query = from l in _locationRepository.GetAll()
                                .AsNoTracking()
                                .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                                .WhereIf(input.Creators != null && input.Creators.Ids != null && input.Creators.Ids.Any(), s =>
                                    (input.Creators.Exclude && (!s.CreatorUserId.HasValue || !input.Creators.Ids.Contains(s.CreatorUserId))) ||
                                    (!input.Creators.Exclude && input.Creators.Ids.Contains(s.CreatorUserId)))
                                .WhereIf(input.Modifiers != null && input.Modifiers.Ids != null && input.Modifiers.Ids.Any(), s =>
                                    (input.Modifiers.Exclude && (!s.LastModifierUserId.HasValue || !input.Modifiers.Ids.Contains(s.LastModifierUserId))) ||
                                    (!input.Modifiers.Exclude && input.Modifiers.Ids.Contains(s.LastModifierUserId)))
                                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s =>
                                    s.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                                    s.DisplayName.ToLower().Contains(input.Keyword.ToLower()))
                        join u in _userRepository.GetAll().AsNoTracking()
                        on l.CreatorUserId equals u.Id
                        join m in _userRepository.GetAll().AsNoTracking()
                        on l.LastModifierUserId equals m.Id
                        into modify
                        from m in modify.DefaultIfEmpty()
                        select new LocationListDto
                        {
                            Id = l.Id,
                            No = l.No,
                            Name = l.Name,
                            DisplayName = l.DisplayName,
                            CannotDelete = l.CannotDelete,
                            CannotEdit = l.CannotEdit,
                            IsActive = l.IsActive,
                            Latitude = l.Latitude,
                            Longitude = l.Longitude,
                            CreationTime = l.CreationTime,
                            CreatorUserId = l.CreatorUserId,
                            CreatorUserName = u.UserName,
                            LastModifierUserId = u.LastModifierUserId,
                            LastModificationTime = l.LastModificationTime,
                            LastModifierUserName = m == null ? "" : m.UserName,
                        };

            var totalCount = await query.CountAsync();
            var items = new List<LocationListDto>();
            if (totalCount > 0)
            {
                query = query.OrderBy(input.GetOrdering());
                if (input.UsePagination) query = query.PageBy(input);
                items = await query.ToListAsync();
            }

            return new PagedResultDto<LocationListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_ExportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcel(ExportExcelLocationInputDto input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
            var result = new ExportFileOutput
            {
                FileName = "Location.xlsx",
                FileToken = $"{Guid.NewGuid()}.xlsx"
            };

            PagedResultDto<LocationListDto> listResult;
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    listResult = await GetListHelper(input);
                }
            }

            using (var p = new ExcelPackage())
            {
                var ws = p.CreateSheet(result.FileName.RemoveExtension());

                #region Row 1 Header Table
                int rowTableHeader = 1;
                //int colHeaderTable = 1;

                // write header collumn table
                var displayColumns = input.Columns.
                    Where(s => s.Visible)
                    .OrderBy(s => s.Index)
                    .ToList();

                //foreach (var i in displayColumns)
                //{
                //    ws.AddTextToCell(rowTableHeader, colHeaderTable, i.ColumnTitle, true);
                //    if (i.Width > 0) ws.Column(colHeaderTable).Width = i.Width.PixcelToInches();

                //    colHeaderTable += 1;
                //}
                #endregion Row 1

                var rowIndex = rowTableHeader + 1;
                foreach (var row in listResult.Items)
                {
                    var colIndex = 1;
                    foreach (var col in displayColumns)
                    {
                        var value = row.GetType().GetProperty(col.ColumnName.ToPascalCase()).GetValue(row);

                        if (col.ColumnName == "CreatorUserName")
                        {
                            var newValue = value;
                            if(row.CreationTime.HasValue) newValue += $"\r\n{Convert.ToDateTime(row.CreationTime).ToString("yyyy-MM-dd HH:mm:ss")}";

                            col.WriteCell(ws, rowIndex, colIndex, newValue);
                        }
                        else if (col.ColumnName == "LastModifierUserName")
                        {
                            var newValue = value;
                            if(row.LastModificationTime.HasValue) newValue += $"\r\n{Convert.ToDateTime(row.LastModificationTime).ToString("yyyy-MM-dd HH:mm:ss")}";

                            col.WriteCell(ws, rowIndex, colIndex, newValue);
                        }
                        else
                        {
                            col.WriteCell(ws, rowIndex, colIndex, value);
                        }

                        colIndex++;
                    }
                    rowIndex++;
                }

                ws.InsertTable(displayColumns, $"{ws.Name}Table", rowTableHeader, 1, rowIndex - 1);

                result.FileUrl = $"{_appFolders.DownloadUrl}?fileName={result.FileName}&fileToken={result.FileToken}";

                await _fileStorageManager.UploadTempFile(result.FileToken, p);
            }

            return result;

        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcelTemplate()
        {
            return await _locationManager.ExportExcelTemplateAsync();
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task ImportExcel(FileTokenInput input)
        {
            var entity = MapEntity<ImportExcelEntity<Guid>, Guid>(input);

            CheckErrors(await _locationManager.ImportExcelAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Edit)]
        public async Task Update(CreateUpdateLocationInputDto input)
        {
            var entity = MapEntity<Location, Guid>(input);

            CheckErrors(await _locationManager.UpdateAsync(entity));
        }
    }
}
