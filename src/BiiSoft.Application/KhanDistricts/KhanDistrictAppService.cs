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
using Abp.Domain.Uow;
using System.Transactions;
using BiiSoft.Locations;
using BiiSoft.KhanDistricts.Dto;
using BiiSoft.Entities;
using BiiSoft.BFiles.Dto;
using BiiSoft.Excels;

namespace BiiSoft.KhanDistricts
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class KhanDistrictAppService : BiiSoftAppServiceBase, IKhanDistrictAppService
    {
        private readonly IKhanDistrictManager _khanDistrictManager;
        private readonly IBiiSoftRepository<KhanDistrict, Guid> _khanDistrictRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IExcelManager _excelManager;
        public KhanDistrictAppService(
            IExcelManager excelManager,
            IUnitOfWorkManager unitOfWorkManager,
            IKhanDistrictManager khanDistrictManager,
            IBiiSoftRepository<KhanDistrict, Guid> khanDistrictRepository,
            IBiiSoftRepository<User, long> userRepository)
        {
            _khanDistrictManager=khanDistrictManager;
            _khanDistrictRepository=khanDistrictRepository;
            _userRepository=userRepository;
            _unitOfWorkManager=unitOfWorkManager;
            _excelManager=excelManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_KhanDistricts_Create)]
        public async Task<Guid> Create(CreateUpdateKhanDistrictInputDto input)
        {
            var entity = MapEntity<KhanDistrict, Guid>(input);
            
            CheckErrors(await _khanDistrictManager.InsertAsync(entity));
            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_KhanDistricts_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            CheckErrors(await _khanDistrictManager.DeleteAsync(input.Id));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_KhanDistricts_Disable)]
        public async Task Disable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _khanDistrictManager.DisableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_KhanDistricts_Enable)]
        public async Task Enable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _khanDistrictManager.EnableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Find_KhanDistricts)]
        public async Task<PagedResultDto<FindKhanDistrictDto>> Find(PageKhanDistrictInputDto input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _khanDistrictRepository.GetAll()
                        .AsNoTracking()
                        .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                        .WhereIf(input.Countries != null && input.Countries.Ids != null && input.Countries.Ids.Any(), s =>
                            (input.Countries.Exclude && (!s.CountryId.HasValue || !input.Countries.Ids.Contains(s.CountryId.Value))) ||
                            (!input.Countries.Exclude && input.Countries.Ids.Contains(s.CountryId.Value)))
                        .WhereIf(input.CityProvinces != null && input.CityProvinces.Ids != null && input.CityProvinces.Ids.Any(), s =>
                            (input.CityProvinces.Exclude && (!s.CityProvinceId.HasValue || !input.CityProvinces.Ids.Contains(s.CityProvinceId.Value))) ||
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
                            s.DisplayName.ToLower().Contains(input.Keyword.ToLower()));
                        

            var totalCount = await query.CountAsync();
            var items = new List<FindKhanDistrictDto>();
            if (totalCount > 0)
            {
                var selectQuery = query
                .Select(l => new FindKhanDistrictDto
                {
                    Id = l.Id,
                    Code = l.Code,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    CountryName = !l.CountryId.HasValue ? "" : isDefaultLanguage ? l.Country.Name : l.Country.DisplayName,
                    CityProvinceName = !l.CityProvinceId.HasValue ? "" : isDefaultLanguage ? l.CityProvince.Name : l.CityProvince.DisplayName,
                    IsActive = l.IsActive,
                });

                if (input.UsePagination)
                {
                    items = await selectQuery.OrderBy(input.GetOrdering()).PageBy(input).ToListAsync();
                }
                else
                {
                    items = await selectQuery.OrderBy(input.GetOrdering()).ToListAsync();
                }
            }

            return new PagedResultDto<FindKhanDistrictDto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_KhanDistricts_View, PermissionNames.Pages_Setup_Locations_KhanDistricts_Edit)]
        public async Task<KhanDistrictDetailDto> GetDetail(EntityDto<Guid> input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _khanDistrictRepository.GetAll()
                        .AsNoTracking()
                        .Where(s => s.Id == input.Id)
                        .Select(l => new KhanDistrictDetailDto
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
                            CreatorUserName = l.CreatorUserId.HasValue ? l.CreatorUser.UserName : "",
                            LastModificationTime = l.LastModificationTime,
                            LastModifierUserId = l.LastModifierUserId,
                            LastModifierUserName = l.LastModifierUserId.HasValue ? l.LastModifierUser.UserName : "",
                            CountryId = l.CountryId,
                            CityProvinceId = l.CityProvinceId,
                            CountryName = !l.CountryId.HasValue ? "" : isDefaultLanguage ? l.Country.Name : l.Country.DisplayName,                          
                            CityProvinceName = !l.CityProvinceId.HasValue ? "" : isDefaultLanguage ? l.CityProvince.Name : l.CityProvince.DisplayName
                        });

            var result = await query.FirstOrDefaultAsync();
            if (result == null) throw new UserFriendlyException(L("RecordNotFound"));

            await _khanDistrictManager.MapNavigation(result);

            return result;
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_KhanDistricts)]
        public async Task<PagedResultDto<KhanDistrictListDto>> GetList(PageKhanDistrictInputDto input)
        {
            return await GetListHelper(input);
        }

        private async Task<PagedResultDto<KhanDistrictListDto>> GetListHelper(PageKhanDistrictInputDto input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _khanDistrictRepository.GetAll()
                        .AsNoTracking()
                        .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                        .WhereIf(input.Countries != null && input.Countries.Ids != null && input.Countries.Ids.Any(), s =>
                            (input.Countries.Exclude && (!s.CountryId.HasValue || !input.Countries.Ids.Contains(s.CountryId.Value))) ||
                            (!input.Countries.Exclude && input.Countries.Ids.Contains(s.CountryId.Value)))
                        .WhereIf(input.CityProvinces != null && input.CityProvinces.Ids != null && input.CityProvinces.Ids.Any(), s =>
                            (input.CityProvinces.Exclude && (!s.CityProvinceId.HasValue || !input.CityProvinces.Ids.Contains(s.CityProvinceId.Value))) ||
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
                            s.DisplayName.ToLower().Contains(input.Keyword.ToLower()));
                        

            var totalCount = await query.CountAsync();
            var items = new List<KhanDistrictListDto>();
            if (totalCount > 0)
            {
                var selectQuery = query
                .Select(l => new KhanDistrictListDto
                {
                    Id = l.Id,
                    No = l.No,
                    Code = l.Code,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    CannotDelete = l.CannotDelete,
                    CannotEdit = l.CannotEdit,
                    IsActive = l.IsActive,
                    CreationTime = l.CreationTime,
                    CreatorUserId = l.CreatorUserId,
                    CreatorUserName = l.CreatorUserId.HasValue ? l.CreatorUser.UserName : "",
                    LastModificationTime = l.LastModificationTime,
                    LastModifierUserId = l.LastModifierUserId,
                    LastModifierUserName = l.LastModifierUserId.HasValue ? l.LastModifierUser.UserName : "",
                    CountryName = !l.CountryId.HasValue ? "" : isDefaultLanguage ? l.Country.Name : l.Country.DisplayName,
                    CityProvinceName = !l.CityProvinceId.HasValue ? "" : isDefaultLanguage ? l.CityProvince.Name : l.CityProvince.DisplayName
                });

                if (input.UsePagination)
                {
                    items = await selectQuery.OrderBy(input.GetOrdering()).PageBy(input).ToListAsync();
                }
                else
                {
                    items = await selectQuery.OrderBy(input.GetOrdering()).ToListAsync();
                }
            }

            return new PagedResultDto<KhanDistrictListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_KhanDistricts_ExportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcel(ExportExcelKhanDistrictInputDto input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
         
            PagedResultDto<KhanDistrictListDto> listResult;
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    listResult = await GetListHelper(input);
                }
            }

            var excelInput = new ExportDataFileInput
            {
                FileName = "KhanDistrict.xlsx",
                Items = listResult.Items,
                Columns = input.Columns
            };

            return await _excelManager.ExportExcelAsync(excelInput);

        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_KhanDistricts_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcelTemplate()
        {
            return await _khanDistrictManager.ExportExcelTemplateAsync();
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_KhanDistricts_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task ImportExcel(FileTokenInput input)
        {
            var entity = MapEntity<ImportExcelEntity<Guid>, Guid>(input);

            CheckErrors(await _khanDistrictManager.ImportExcelAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_KhanDistricts_Edit)]
        public async Task Update(CreateUpdateKhanDistrictInputDto input)
        {
            var entity = MapEntity<KhanDistrict, Guid>(input);

            CheckErrors(await _khanDistrictManager.UpdateAsync(entity));
        }
    }
}
