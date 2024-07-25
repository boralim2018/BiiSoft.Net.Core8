﻿using Abp.Application.Services.Dto;
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
using BiiSoft.Locations;
using BiiSoft.SangkatCommunes.Dto;

namespace BiiSoft.SangkatCommunes
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class SangkatCommuneAppService : BiiSoftAppServiceBase, ISangkatCommuneAppService
    {
        private readonly ISangkatCommuneManager _sangkatCommuneManager;
        private readonly IBiiSoftRepository<SangkatCommune, Guid> _sangkatCommuneRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IAppFolders _appFolders;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SangkatCommuneAppService(
            IUnitOfWorkManager unitOfWorkManager,
            IFileStorageManager fileStorageManager,
            IAppFolders appFolders,
            ISangkatCommuneManager sangkatCommuneManager,
            IBiiSoftRepository<SangkatCommune, Guid> sangkatCommuneRepository,
            IBiiSoftRepository<User, long> userRepository)
        {
            _sangkatCommuneManager=sangkatCommuneManager;
            _sangkatCommuneRepository=sangkatCommuneRepository;
            _userRepository=userRepository;
            _fileStorageManager=fileStorageManager;
            _appFolders=appFolders;
            _unitOfWorkManager=unitOfWorkManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_SangkatCommunes_Create)]
        public async Task<Guid> Create(CreateUpdateSangkatCommuneInputDto input)
        {
            var entity = ObjectMapper.Map<SangkatCommune>(input);
            
            await _sangkatCommuneManager.InsertAsync(AbpSession.TenantId, AbpSession.UserId.Value, entity);
            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_SangkatCommunes_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _sangkatCommuneManager.DeleteAsync(input.Id);
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_SangkatCommunes_Disable)]
        public async Task Disable(EntityDto<Guid> input)
        {
            await _sangkatCommuneManager.DisableAsync(AbpSession.UserId.Value, input.Id);
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_SangkatCommunes_Enable)]
        public async Task Enable(EntityDto<Guid> input)
        {
            await _sangkatCommuneManager.EnableAsync(AbpSession.UserId.Value, input.Id);
        }

        [AbpAuthorize(PermissionNames.Pages_Find_SangkatCommunes)]
        public async Task<PagedResultDto<FindSangkatCommuneDto>> Find(PageSangkatCommuneInputDto input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = from l in _sangkatCommuneRepository.GetAll()
                                .AsNoTracking()
                                .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                                .WhereIf(input.Countries != null && input.Countries.Ids != null && input.Countries.Ids.Any(), s =>
                                    (input.Countries.Exclude && (!s.CountryId.HasValue || !input.Countries.Ids.Contains(s.CountryId.Value))) ||
                                    (!input.Countries.Exclude && input.Countries.Ids.Contains(s.CountryId.Value)))
                                .WhereIf(input.CityProvinces != null && input.CityProvinces.Ids != null && input.CityProvinces.Ids.Any(), s =>
                                    (input.CityProvinces.Exclude && (!s.CityProvinceId.HasValue || !input.CityProvinces.Ids.Contains(s.CityProvinceId.Value))) ||
                                    (!input.CityProvinces.Exclude && input.CityProvinces.Ids.Contains(s.CityProvinceId.Value)))
                                .WhereIf(input.KhanDistricts != null && input.KhanDistricts.Ids != null && input.KhanDistricts.Ids.Any(), s =>
                                    (input.KhanDistricts.Exclude && (!s.KhanDistrictId.HasValue || !input.KhanDistricts.Ids.Contains(s.KhanDistrictId.Value))) ||
                                    (!input.CityProvinces.Exclude && input.CityProvinces.Ids.Contains(s.CityProvinceId.Value)))
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
                        select new FindSangkatCommuneDto
                        {
                            Id = l.Id,
                            Code = l.Code,
                            Name = l.Name,
                            DisplayName = l.DisplayName,
                            CountryName = !l.CountryId.HasValue ? "" : isDefaultLanguage ? l.Country.Name : l.Country.DisplayName,
                            CityProvinceName = !l.CityProvinceId.HasValue ? "" : isDefaultLanguage ? l.CityProvince.Name : l.CityProvince.DisplayName,
                            KhanDistrictName = !l.KhanDistrictId.HasValue ? "" : isDefaultLanguage ? l.KhanDistrict.Name : l.KhanDistrict.DisplayName,
                            IsActive = l.IsActive,
                        };

            var totalCount = await query.CountAsync();
            var items = new List<FindSangkatCommuneDto>();
            if (totalCount > 0)
            {
                query = query.OrderBy(input.GetOrdering());
                if (input.UsePagination) query = query.PageBy(input);
                items = await query.ToListAsync();
            }

            return new PagedResultDto<FindSangkatCommuneDto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_SangkatCommunes_View, PermissionNames.Pages_Setup_Locations_SangkatCommunes_Edit)]
        public async Task<SangkatCommuneDetailDto> GetDetail(EntityDto<Guid> input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = from l in _sangkatCommuneRepository.GetAll()
                                .AsNoTracking()
                                .Where(s => s.Id == input.Id)
                        join u in _userRepository.GetAll().AsNoTracking()
                        on l.CreatorUserId equals u.Id
                        join m in _userRepository.GetAll().AsNoTracking()
                        on l.LastModifierUserId equals m.Id
                        into modify
                        from m in modify.DefaultIfEmpty()
                        select new SangkatCommuneDetailDto
                        {
                            Id = l.Id,
                            No = l.No,
                            Name = l.Name,
                            DisplayName = l.DisplayName,
                            CannotDelete = l.CannotDelete,
                            CannotEdit = l.CannotEdit,
                            Code = l.Code,
                            IsActive = l.IsActive,
                            Latitude = l.Latitude,
                            Longitude = l.Longitude,
                            CreationTime = l.CreationTime,
                            CreatorUserId = l.CreatorUserId,
                            CreatorUserName = u.UserName,
                            LastModificationTime = l.LastModificationTime,
                            LastModifierUserId = l.LastModifierUserId,
                            LastModifierUserName = m == null ? "" : m.UserName,
                            CountryId = l.CountryId,
                            CityProvinceId = l.CityProvinceId,
                            KhanDistrictId = l.KhanDistrictId,
                            CountryName = !l.CountryId.HasValue ? "" : isDefaultLanguage ? l.Country.Name : l.Country.DisplayName,
                            CityProvinceName = !l.CityProvinceId.HasValue ? "" : isDefaultLanguage ? l.CityProvince.Name : l.CityProvince.DisplayName,
                            KhanDistrictName = !l.KhanDistrictId.HasValue ? "" : isDefaultLanguage ? l.KhanDistrict.Name : l.KhanDistrict.DisplayName,
                        };

            var result = await query.FirstOrDefaultAsync();
            if (result == null) throw new UserFriendlyException(L("RecordNotFound"));

            var record = await _sangkatCommuneRepository.GetAll()
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


        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_SangkatCommunes)]
        public async Task<PagedResultDto<SangkatCommuneListDto>> GetList(PageSangkatCommuneInputDto input)
        {
            return await GetListHelper(input);
        }

        private async Task<PagedResultDto<SangkatCommuneListDto>> GetListHelper(PageSangkatCommuneInputDto input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = from l in _sangkatCommuneRepository.GetAll()
                                .AsNoTracking()
                                .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                                .WhereIf(input.Countries != null && input.Countries.Ids != null && input.Countries.Ids.Any(), s =>
                                    (input.Countries.Exclude && (!s.CountryId.HasValue || !input.Countries.Ids.Contains(s.CountryId.Value))) ||
                                    (!input.Countries.Exclude && input.Countries.Ids.Contains(s.CountryId.Value)))
                                .WhereIf(input.CityProvinces != null && input.CityProvinces.Ids != null && input.CityProvinces.Ids.Any(), s =>
                                    (input.CityProvinces.Exclude && (!s.CityProvinceId.HasValue || !input.CityProvinces.Ids.Contains(s.CityProvinceId.Value))) ||
                                    (!input.CityProvinces.Exclude && input.CityProvinces.Ids.Contains(s.CityProvinceId.Value)))
                                .WhereIf(input.KhanDistricts != null && input.KhanDistricts.Ids != null && input.KhanDistricts.Ids.Any(), s =>
                                    (input.KhanDistricts.Exclude && (!s.KhanDistrictId.HasValue || !input.KhanDistricts.Ids.Contains(s.KhanDistrictId.Value))) ||
                                    (!input.CityProvinces.Exclude && input.CityProvinces.Ids.Contains(s.CityProvinceId.Value)))
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
                        select new SangkatCommuneListDto
                        {
                            Id = l.Id,
                            No = l.No,
                            Code = l.Code,
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
                            CountryName = !l.CountryId.HasValue ? "" : isDefaultLanguage ? l.Country.Name : l.Country.DisplayName,
                            CityProvinceName = !l.CityProvinceId.HasValue ? "" : isDefaultLanguage ? l.CityProvince.Name : l.CityProvince.DisplayName,
                            KhanDistrictName = !l.KhanDistrictId.HasValue ? "" : isDefaultLanguage ? l.KhanDistrict.Name : l.KhanDistrict.DisplayName,
                        };

            var totalCount = await query.CountAsync();
            var items = new List<SangkatCommuneListDto>();
            if (totalCount > 0)
            {
                query = query.OrderBy(input.GetOrdering());
                if (input.UsePagination) query = query.PageBy(input);
                items = await query.ToListAsync();
            }

            return new PagedResultDto<SangkatCommuneListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_SangkatCommunes_ExportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcel(ExportExcelSangkatCommuneInputDto input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
            var result = new ExportFileOutput
            {
                FileName = "SangkatCommune.xlsx",
                FileToken = $"{Guid.NewGuid()}.xlsx"
            };

            PagedResultDto<SangkatCommuneListDto> listResult;
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

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_SangkatCommunes_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcelTemplate()
        {
            var result = new ExportFileOutput
            {
                FileName = "SangkatCommune.xlsx",
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
                    new ColumnOutput{ ColumnTitle = L("LocationCode"), Width = 200, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Name_",L("SangkatCommune")), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("DisplayName"), Width = 250, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Country"), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("CityProvince"), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("KhanDistrict"), Width = 150, IsRequired = true },
                    new ColumnOutput{ ColumnTitle = L("Latitude"), Width = 150 },
                    new ColumnOutput{ ColumnTitle = L("Longitude"), Width = 150 },
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

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_SangkatCommunes_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task ImportExcel(FileTokenInput input)
        {
            await _sangkatCommuneManager.ImportAsync(AbpSession.UserId.Value, input.Token);
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_SangkatCommunes_Edit)]
        public async Task Update(CreateUpdateSangkatCommuneInputDto input)
        {
            var entity = ObjectMapper.Map<SangkatCommune>(input);
            await _sangkatCommuneManager.UpdateAsync(AbpSession.UserId.Value, entity);
        }
    }
}
