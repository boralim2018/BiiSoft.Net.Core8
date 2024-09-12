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
using BiiSoft.SangkatCommunes.Dto;
using BiiSoft.Entities;
using BiiSoft.BFiles.Dto;

namespace BiiSoft.SangkatCommunes
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class SangkatCommuneAppService : BiiSoftExcelAppServiceBase, ISangkatCommuneAppService
    {
        private readonly ISangkatCommuneManager _sangkatCommuneManager;
        private readonly IBiiSoftRepository<SangkatCommune, Guid> _sangkatCommuneRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SangkatCommuneAppService(
            IUnitOfWorkManager unitOfWorkManager,
            ISangkatCommuneManager sangkatCommuneManager,
            IBiiSoftRepository<SangkatCommune, Guid> sangkatCommuneRepository,
            IBiiSoftRepository<User, long> userRepository)
        {
            _sangkatCommuneManager = sangkatCommuneManager;
            _sangkatCommuneRepository = sangkatCommuneRepository;
            _userRepository = userRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_SangkatCommunes_Create)]
        public async Task<Guid> Create(CreateUpdateSangkatCommuneInputDto input)
        {
            var entity = MapEntity<SangkatCommune, Guid>(input);

            CheckErrors(await _sangkatCommuneManager.InsertAsync(entity));
            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_SangkatCommunes_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            CheckErrors(await _sangkatCommuneManager.DeleteAsync(input.Id));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_SangkatCommunes_Disable)]
        public async Task Disable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _sangkatCommuneManager.DisableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_SangkatCommunes_Enable)]
        public async Task Enable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _sangkatCommuneManager.EnableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Find_SangkatCommunes)]
        public async Task<PagedResultDto<FindSangkatCommuneDto>> Find(PageSangkatCommuneInputDto input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _sangkatCommuneRepository.GetAll()
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
                            s.DisplayName.ToLower().Contains(input.Keyword.ToLower()));
                        

            var totalCount = await query.CountAsync();
            var items = new List<FindSangkatCommuneDto>();
            if (totalCount > 0)
            {
                var selectQuery = query.OrderBy(input.GetOrdering())
                .Select(l => new FindSangkatCommuneDto
                {
                    Id = l.Id,
                    Code = l.Code,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    CountryName = !l.CountryId.HasValue ? "" : isDefaultLanguage ? l.Country.Name : l.Country.DisplayName,
                    CityProvinceName = !l.CityProvinceId.HasValue ? "" : isDefaultLanguage ? l.CityProvince.Name : l.CityProvince.DisplayName,
                    KhanDistrictName = !l.KhanDistrictId.HasValue ? "" : isDefaultLanguage ? l.KhanDistrict.Name : l.KhanDistrict.DisplayName,
                    IsActive = l.IsActive,
                });

                if (input.UsePagination) selectQuery = selectQuery.PageBy(input);

                items = await selectQuery.ToListAsync();
            }

            return new PagedResultDto<FindSangkatCommuneDto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_SangkatCommunes_View, PermissionNames.Pages_Setup_Locations_SangkatCommunes_Edit)]
        public async Task<SangkatCommuneDetailDto> GetDetail(EntityDto<Guid> input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _sangkatCommuneRepository.GetAll()
                                .AsNoTracking()
                                .Where(s => s.Id == input.Id)
                        .Select(l => new SangkatCommuneDetailDto
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
                            CountryName = !l.CountryId.HasValue ? "" : isDefaultLanguage ? l.Country.Name : l.Country.DisplayName,
                            CityProvinceName = !l.CityProvinceId.HasValue ? "" : isDefaultLanguage ? l.CityProvince.Name : l.CityProvince.DisplayName,
                            KhanDistrictName = !l.KhanDistrictId.HasValue ? "" : isDefaultLanguage ? l.KhanDistrict.Name : l.KhanDistrict.DisplayName,
                        });

            var result = await query.FirstOrDefaultAsync();
            if (result == null) throw new UserFriendlyException(L("RecordNotFound"));

            await _sangkatCommuneManager.MapNavigation(result);

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

            var query = _sangkatCommuneRepository.GetAll()
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
                            s.DisplayName.ToLower().Contains(input.Keyword.ToLower()));
                        

            var totalCount = await query.CountAsync();
            var items = new List<SangkatCommuneListDto>();
            if (totalCount > 0)
            {
                var selectQuery = query.OrderBy(input.GetOrdering())
                .Select(l => new SangkatCommuneListDto
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
                });

                if (input.UsePagination) selectQuery = selectQuery.PageBy(input);

                items = await selectQuery.ToListAsync();
            }

            return new PagedResultDto<SangkatCommuneListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_SangkatCommunes_ExportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcel(ExportExcelSangkatCommuneInputDto input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
         
            PagedResultDto<SangkatCommuneListDto> listResult;
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    listResult = await GetListHelper(input);
                }
            }

            var excelInput = new ExportFileInput
            {
                FileName = "SangkatCommune.xlsx",
                Items = listResult.Items,
                Columns = input.Columns
            };

            return await ExportExcelAsync(excelInput);

        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_SangkatCommunes_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcelTemplate()
        {
            return await _sangkatCommuneManager.ExportExcelTemplateAsync();
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_SangkatCommunes_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task ImportExcel(FileTokenInput input)
        {
            var entity = MapEntity<ImportExcelEntity<Guid>, Guid>(input);

            CheckErrors(await _sangkatCommuneManager.ImportExcelAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_SangkatCommunes_Edit)]
        public async Task Update(CreateUpdateSangkatCommuneInputDto input)
        {
            var entity = MapEntity<SangkatCommune, Guid>(input);

            CheckErrors(await _sangkatCommuneManager.UpdateAsync(entity));
        }
    }
}
