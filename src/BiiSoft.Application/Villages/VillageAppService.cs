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
using Abp.Domain.Uow;
using System.Transactions;
using BiiSoft.Locations;
using BiiSoft.Villages.Dto;
using BiiSoft.Entities;
using BiiSoft.BFiles.Dto;

namespace BiiSoft.Villages
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class VillageAppService : BiiSoftExcelAppServiceBase, IVillageAppService
    {
        private readonly IVillageManager _villageManager;
        private readonly IBiiSoftRepository<Village, Guid> _villageRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public VillageAppService(
            IUnitOfWorkManager unitOfWorkManager,
            IVillageManager villageManager,
            IBiiSoftRepository<Village, Guid> villageRepository,
            IBiiSoftRepository<User, long> userRepository)
        {
            _villageManager=villageManager;
            _villageRepository=villageRepository;
            _userRepository=userRepository;
            _unitOfWorkManager=unitOfWorkManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Villages_Create)]
        public async Task<Guid> Create(CreateUpdateVillageInputDto input)
        {
            var entity = MapEntity<Village, Guid>(input);
            
            CheckErrors(await _villageManager.InsertAsync(entity));
            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Villages_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            CheckErrors(await _villageManager.DeleteAsync(input.Id));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Villages_Disable)]
        public async Task Disable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _villageManager.DisableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Villages_Enable)]
        public async Task Enable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _villageManager.EnableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Find_Villages)]
        public async Task<PagedResultDto<FindVillageDto>> Find(PageVillageInputDto input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _villageRepository.GetAll()
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
                        .WhereIf(input.SangkatCommunes != null && input.SangkatCommunes.Ids != null && input.SangkatCommunes.Ids.Any(), s =>
                            (input.SangkatCommunes.Exclude && (!s.SangkatCommuneId.HasValue || !input.SangkatCommunes.Ids.Contains(s.SangkatCommuneId.Value))) ||
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
            var items = new List<FindVillageDto>();
            if (totalCount > 0)
            {
                var selectQuery = query.OrderBy(input.GetOrdering())
                .Select(l => new FindVillageDto
                {
                    Id = l.Id,
                    Code = l.Code,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    CountryName = !l.CountryId.HasValue ? "" : isDefaultLanguage ? l.Country.Name : l.Country.DisplayName,
                    CityProvinceName = !l.CityProvinceId.HasValue ? "" : isDefaultLanguage ? l.CityProvince.Name : l.CityProvince.DisplayName,
                    KhanDistrictName = !l.KhanDistrictId.HasValue ? "" : isDefaultLanguage ? l.KhanDistrict.Name : l.KhanDistrict.DisplayName,
                    SangkatCommuneName = !l.SangkatCommuneId.HasValue ? "" : isDefaultLanguage ? l.SangkatCommune.Name : l.SangkatCommune.DisplayName,
                    IsActive = l.IsActive,
                });

                if (input.UsePagination) selectQuery = selectQuery.PageBy(input);

                items = await selectQuery.ToListAsync();
            }

            return new PagedResultDto<FindVillageDto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Villages_View, PermissionNames.Pages_Setup_Locations_Villages_Edit)]
        public async Task<VillageDetailDto> GetDetail(EntityDto<Guid> input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _villageRepository.GetAll()
                        .AsNoTracking()
                        .Where(s => s.Id == input.Id)
                        .Select(l => new VillageDetailDto
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
                            KhanDistrictId = l.KhanDistrictId,
                            SangkatCommuneId = l.SangkatCommuneId,
                            CountryName = !l.CountryId.HasValue ? "" : isDefaultLanguage ? l.Country.Name : l.Country.DisplayName,
                            CityProvinceName = !l.CityProvinceId.HasValue ? "" : isDefaultLanguage ? l.CityProvince.Name : l.CityProvince.DisplayName,
                            KhanDistrictName = !l.KhanDistrictId.HasValue ? "" : isDefaultLanguage ? l.KhanDistrict.Name : l.KhanDistrict.DisplayName,
                            SangkatCommuneName = !l.SangkatCommuneId.HasValue ? "" : isDefaultLanguage ? l.SangkatCommune.Name : l.SangkatCommune.DisplayName,
                        });

            var result = await query.FirstOrDefaultAsync();
            if (result == null) throw new UserFriendlyException(L("RecordNotFound"));

            await _villageManager.MapNavigation(result);

            return result;
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Villages)]
        public async Task<PagedResultDto<VillageListDto>> GetList(PageVillageInputDto input)
        {
            return await GetListHelper(input);
        }

        private async Task<PagedResultDto<VillageListDto>> GetListHelper(PageVillageInputDto input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _villageRepository.GetAll()
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
                        .WhereIf(input.SangkatCommunes != null && input.SangkatCommunes.Ids != null && input.SangkatCommunes.Ids.Any(), s =>
                            (input.SangkatCommunes.Exclude && (!s.SangkatCommuneId.HasValue || !input.SangkatCommunes.Ids.Contains(s.SangkatCommuneId.Value))) ||
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
            var items = new List<VillageListDto>();
            if (totalCount > 0)
            {
                var selectQuery = query.OrderBy(input.GetOrdering())
                .Select(l => new VillageListDto
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
                    CityProvinceName = !l.CityProvinceId.HasValue ? "" : isDefaultLanguage ? l.CityProvince.Name : l.CityProvince.DisplayName,
                    KhanDistrictName = !l.KhanDistrictId.HasValue ? "" : isDefaultLanguage ? l.KhanDistrict.Name : l.KhanDistrict.DisplayName,
                    SangkatCommuneName = !l.SangkatCommuneId.HasValue ? "" : isDefaultLanguage ? l.SangkatCommune.Name : l.SangkatCommune.DisplayName,
                });

                if (input.UsePagination) selectQuery = selectQuery.PageBy(input);

                items = await selectQuery.ToListAsync();
            }

            return new PagedResultDto<VillageListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Villages_ExportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcel(ExportExcelVillageInputDto input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
           
            PagedResultDto<VillageListDto> listResult;
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    listResult = await GetListHelper(input);
                }
            }

            var excelInput = new ExportFileInput
            {
                FileName = "Village.xlsx",
                Items = listResult.Items,
                Columns = input.Columns
            };

            return await ExportExcelAsync(excelInput);

        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Villages_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcelTemplate()
        {
            return await _villageManager.ExportExcelTemplateAsync();
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Villages_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task ImportExcel(FileTokenInput input)
        {
            var entity = MapEntity<ImportExcelEntity<Guid>, Guid>(input);

            CheckErrors(await _villageManager.ImportExcelAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Villages_Edit)]
        public async Task Update(CreateUpdateVillageInputDto input)
        {
            var entity = MapEntity<Village, Guid>(input);

            CheckErrors(await _villageManager.UpdateAsync(entity));
        }
    }
}
