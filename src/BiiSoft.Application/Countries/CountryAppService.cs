using Abp.Application.Services.Dto;
using Abp.Authorization;
using BiiSoft.Authorization;
using BiiSoft.BFiles;
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
using Abp.Runtime.Session;
using BiiSoft.Locations;
using BiiSoft.Countries.Dto;
using BiiSoft.Currencies;
using BiiSoft.Entities;

namespace BiiSoft.Countries
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class CountryAppService : BiiSoftAppServiceBase, ICountryAppService
    {
        private readonly ICountryManager _countryManager;
        private readonly IBiiSoftRepository<Country, Guid> _countryRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IAppFolders _appFolders;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        
        public CountryAppService(
            IUnitOfWorkManager unitOfWorkManager,
            IFileStorageManager fileStorageManager,
            IAppFolders appFolders,
            ICountryManager countryManager,
            IBiiSoftRepository<Country, Guid> countryRepository,
            IBiiSoftRepository<User, long> userRepository)
        {
            _countryManager=countryManager;
            _countryRepository=countryRepository;
            _userRepository=userRepository;
            _fileStorageManager=fileStorageManager;
            _appFolders=appFolders;
            _unitOfWorkManager=unitOfWorkManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Countries_Create)]
        public async Task<Guid> Create(CreateUpdateCountryInputDto input)
        {
            var entity = MapEntity<Country, Guid>(input);
            
            CheckErrors(await _countryManager.InsertAsync(entity));
            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Countries_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _countryManager.DeleteAsync(input.Id);
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Countries_Disable)]
        public async Task Disable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _countryManager.DisableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Countries_Enable)]
        public async Task Enable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _countryManager.EnableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Find_Countries)]
        public async Task<PagedResultDto<FindCountryDto>> Find(PageCountryInputDto input)
        {
            var query = from l in _countryRepository.GetAll()
                                .AsNoTracking()
                                .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                                .WhereIf(input.Currencies != null && input.Currencies.Ids != null && input.Currencies.Ids.Any(), s =>
                                    (input.Currencies.Exclude && (!s.CurrencyId.HasValue || !input.Currencies.Ids.Contains(s.CurrencyId.Value))) ||
                                    (!input.Currencies.Exclude && input.Currencies.Ids.Contains(s.CurrencyId.Value)))
                                .WhereIf(input.Creators != null && input.Creators.Ids != null && input.Creators.Ids.Any(), s =>
                                    (input.Creators.Exclude && (!s.CreatorUserId.HasValue || !input.Creators.Ids.Contains(s.CreatorUserId))) ||
                                    (!input.Creators.Exclude && input.Creators.Ids.Contains(s.CreatorUserId)))
                                .WhereIf(input.Modifiers != null && input.Modifiers.Ids != null && input.Modifiers.Ids.Any(), s =>
                                    (input.Modifiers.Exclude && (!s.LastModifierUserId.HasValue || !input.Modifiers.Ids.Contains(s.LastModifierUserId))) ||
                                    (!input.Modifiers.Exclude && input.Modifiers.Ids.Contains(s.LastModifierUserId)))
                                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s =>
                                    s.Code.ToLower().Contains(input.Keyword.ToLower()) ||
                                    s.ISO2.ToLower().Contains(input.Keyword.ToLower()) ||
                                    s.ISO.ToLower().Contains(input.Keyword.ToLower()) ||
                                    s.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                                    s.DisplayName.ToLower().Contains(input.Keyword.ToLower()))
                        select new FindCountryDto
                        {
                            Id = l.Id,
                            Code = l.Code,
                            Name = l.Name,
                            DisplayName = l.DisplayName,
                            ISO = l.ISO,
                            ISO2 = l.ISO2,
                            PhonePrefix = l.PhonePrefix,
                            IsActive = l.IsActive,
                        };

            var totalCount = await query.CountAsync();
            var items = new List<FindCountryDto>();
            if (totalCount > 0)
            {
                query = query.OrderBy(input.GetOrdering());
                if (input.UsePagination) query = query.PageBy(input);
                items = await query.ToListAsync();
            }

            return new PagedResultDto<FindCountryDto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Countries_View, PermissionNames.Pages_Setup_Locations_Countries_Edit)]
        public async Task<CountryDetailDto> GetDetail(EntityDto<Guid> input)
        {
            var query = from l in _countryRepository.GetAll()
                                .AsNoTracking()
                                .Where(s => s.Id == input.Id)
                        join u in _userRepository.GetAll().AsNoTracking()
                        on l.CreatorUserId equals u.Id
                        join m in _userRepository.GetAll().AsNoTracking()
                        on l.LastModifierUserId equals m.Id
                        into modify
                        from m in modify.DefaultIfEmpty()
                        select new CountryDetailDto
                        {
                            Id = l.Id,
                            No = l.No,
                            Name = l.Name,
                            DisplayName = l.DisplayName,
                            CannotDelete = l.CannotDelete,
                            CannotEdit = l.CannotEdit,
                            Code = l.Code,
                            IsActive = l.IsActive,
                            CreationTime = l.CreationTime,
                            CreatorUserId = l.CreatorUserId,
                            CreatorUserName = u.UserName,
                            LastModificationTime = l.LastModificationTime,
                            LastModifierUserId = l.LastModifierUserId,
                            LastModifierUserName = m == null ? "" : m.UserName,
                            CurrencyId = l.CurrencyId,
                            CurrencyCode = l.CurrencyId.HasValue ? l.Currency.Code : "",
                            PhonePrefix = l.PhonePrefix,
                            ISO = l.ISO,
                            ISO2 = l.ISO2
                        };

            var result = await query.FirstOrDefaultAsync();
            if (result == null) throw new UserFriendlyException(L("RecordNotFound"));

            var record = await _countryRepository.GetAll()
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


        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Countries)]
        public async Task<PagedResultDto<CountryListDto>> GetList(PageCountryInputDto input)
        {
            return await GetListHelper(input);
        }

        private async Task<PagedResultDto<CountryListDto>> GetListHelper(PageCountryInputDto input)
        {
            var query = from l in _countryRepository.GetAll()
                                .AsNoTracking()
                                .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                                .WhereIf(input.Currencies != null && input.Currencies.Ids != null && input.Currencies.Ids.Any(), s =>
                                    (input.Currencies.Exclude && (!s.CurrencyId.HasValue || !input.Currencies.Ids.Contains(s.CurrencyId.Value))) ||
                                    (!input.Currencies.Exclude && input.Currencies.Ids.Contains(s.CurrencyId.Value)))
                                .WhereIf(input.Creators != null && input.Creators.Ids != null && input.Creators.Ids.Any(), s =>
                                    (input.Creators.Exclude && (!s.CreatorUserId.HasValue || !input.Creators.Ids.Contains(s.CreatorUserId))) ||
                                    (!input.Creators.Exclude && input.Creators.Ids.Contains(s.CreatorUserId)))
                                .WhereIf(input.Modifiers != null && input.Modifiers.Ids != null && input.Modifiers.Ids.Any(), s =>
                                    (input.Modifiers.Exclude && (!s.LastModifierUserId.HasValue || !input.Modifiers.Ids.Contains(s.LastModifierUserId))) ||
                                    (!input.Modifiers.Exclude && input.Modifiers.Ids.Contains(s.LastModifierUserId)))
                                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s =>
                                    s.Code.ToLower().Contains(input.Keyword.ToLower()) ||
                                    s.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                                    s.DisplayName.ToLower().Contains(input.Keyword.ToLower()))
                        join u in _userRepository.GetAll().AsNoTracking()
                        on l.CreatorUserId equals u.Id
                        join m in _userRepository.GetAll().AsNoTracking()
                        on l.LastModifierUserId equals m.Id
                        into modify
                        from m in modify.DefaultIfEmpty()
                        select new CountryListDto
                        {
                            Id = l.Id,
                            No = l.No,
                            Name = l.Name,
                            Code = l.Code,
                            DisplayName = l.DisplayName,
                            CannotDelete = l.CannotDelete,
                            CannotEdit = l.CannotEdit,
                            IsActive = l.IsActive,
                            CreationTime = l.CreationTime,
                            CreatorUserId = l.CreatorUserId,
                            CreatorUserName = u.UserName,
                            LastModifierUserId = u.LastModifierUserId,
                            LastModificationTime = l.LastModificationTime,
                            LastModifierUserName = m == null ? "" : m.UserName,
                            ISO = l.ISO,
                            ISO2 = l.ISO2,
                            PhonePrefix = l.PhonePrefix,
                            CurrencyCode = l.CurrencyId.HasValue ? l.Currency.Code : "",

                        };

            var totalCount = await query.CountAsync();
            var items = new List<CountryListDto>();
            if (totalCount > 0)
            {
                query = query.OrderBy(input.GetOrdering());
                if (input.UsePagination) query = query.PageBy(input);
                items = await query.ToListAsync();
            }

            return new PagedResultDto<CountryListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Countries_ExportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcel(ExportExcelCountryInputDto input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
            var result = new ExportFileOutput
            {
                FileName = "Country.xlsx",
                FileToken = $"{Guid.NewGuid()}.xlsx"
            };

            PagedResultDto<CountryListDto> listResult;
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
                        var value = row.GetType().GetProperty(col.ColumnName).GetValue(row);

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

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Countries_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcelTemplate()
        {
            var result = new ExportFileOutput
            {
                FileName = "Country.xlsx",
                FileToken = $"{Guid.NewGuid()}.xlsx"
            };

            using (var p = new ExcelPackage())
            {
                var ws = p.CreateSheet(result.FileName.RemoveExtension());

                #region Row 1 Header Table
                int rowTableHeader = 1;
                //int colHeaderTable = 1;

                // write header collumn table
                var displayColumns = new List<ColumnOutput> { 
                    new ColumnOutput{ ColumnTitle = L("Code"), Width = 200, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Name_",L("Country")), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("DisplayName"), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("ISO"), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("ISO2"), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("PhonePrefix"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("Currency"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("CannotEdit"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("CannotDelete"), Width = 150 },
                };

                #endregion Row 1

                ws.InsertTable(displayColumns, $"{ws.Name}Table", rowTableHeader, 1, 5);

                result.FileUrl = $"{_appFolders.DownloadUrl}?fileName={result.FileName}&fileToken={result.FileToken}";

                await _fileStorageManager.UploadTempFile(result.FileToken, p);
            }

            return result;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Countries_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task ImportExcel(FileTokenInput input)
        {
            var entity = MapEntity<ImportExcelEntity<Guid>, Guid>(input);

            CheckErrors(await _countryManager.ImportAsync(entity));

        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Countries_Edit)]
        public async Task Update(CreateUpdateCountryInputDto input)
        {
            var entity = MapEntity<Country, Guid>(input);

            CheckErrors(await _countryManager.UpdateAsync(entity));
        }
    }
}
