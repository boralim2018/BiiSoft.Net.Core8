using Abp.Application.Services.Dto;
using Abp.Authorization;
using BiiSoft.Authorization;
using BiiSoft.BFiles;
using System;
using System.Collections.Generic;
using System.Linq;
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
using BiiSoft.CityProvinces.Dto;
using BiiSoft.Entities;
using BiiSoft.BFiles.Dto;
using BiiSoft.Excels;

namespace BiiSoft.CityProvinces
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class CityProvinceAppService : BiiSoftAppServiceBase, ICityProvinceAppService
    {
        private readonly ICityProvinceManager _cityProvinceManager;
        private readonly IBiiSoftRepository<CityProvince, Guid> _cityProvinceRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IExcelManager _excelManager;

        public CityProvinceAppService(
            IExcelManager excelManager,
            IUnitOfWorkManager unitOfWorkManager,
            ICityProvinceManager cityProvinceManager,
            IBiiSoftRepository<CityProvince, Guid> cityProvinceRepository,
            IBiiSoftRepository<User, long> userRepository)
        {
            _cityProvinceManager=cityProvinceManager;
            _cityProvinceRepository=cityProvinceRepository;
            _userRepository=userRepository;
            _unitOfWorkManager=unitOfWorkManager;
            _excelManager=excelManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_CityProvinces_Create)]
        public async Task<Guid> Create(CreateUpdateCityProvinceInputDto input)
        {
            var entity = MapEntity<CityProvince, Guid>(input);
            
            CheckErrors(await _cityProvinceManager.InsertAsync(entity));
            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_CityProvinces_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            CheckErrors(await _cityProvinceManager.DeleteAsync(input.Id));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_CityProvinces_Disable)]
        public async Task Disable(EntityDto<Guid> input)
        {
            var entiry = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _cityProvinceManager.DisableAsync(entiry));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_CityProvinces_Enable)]
        public async Task Enable(EntityDto<Guid> input)
        {
            var entiry = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _cityProvinceManager.EnableAsync(entiry));
        }

        [AbpAuthorize(PermissionNames.Pages_Find_CityProvinces)]
        public async Task<PagedResultDto<FindCityProvinceDto>> Find(PageCityProvinceInputDto input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _cityProvinceRepository.GetAll()
                        .AsNoTracking()
                        .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                        .WhereIf(input.Countries != null && input.Countries.Ids != null && input.Countries.Ids.Any(), s =>
                            (input.Countries.Exclude && (!s.CountryId.HasValue || !input.Countries.Ids.Contains(s.CountryId.Value))) ||
                            (!input.Countries.Exclude && input.Countries.Ids.Contains(s.CountryId.Value)))
                        .WhereIf(input.Creators != null && input.Creators.Ids != null && input.Creators.Ids.Any(), s =>
                            (input.Creators.Exclude && (!s.CreatorUserId.HasValue || !input.Creators.Ids.Contains(s.CreatorUserId))) ||
                            (!input.Creators.Exclude && input.Creators.Ids.Contains(s.CreatorUserId)))
                        .WhereIf(input.Modifiers != null && input.Modifiers.Ids != null && input.Modifiers.Ids.Any(), s =>
                            (input.Modifiers.Exclude && (!s.LastModifierUserId.HasValue || !input.Modifiers.Ids.Contains(s.LastModifierUserId))) ||
                            (!input.Modifiers.Exclude && input.Modifiers.Ids.Contains(s.LastModifierUserId)))
                        .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s =>
                            s.Code.ToLower().Contains(input.Keyword.ToLower()) ||
                            s.ISO.ToLower().Contains(input.Keyword.ToLower()) ||
                            s.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                            s.DisplayName.ToLower().Contains(input.Keyword.ToLower()));
                       

            var totalCount = await query.CountAsync();
            var items = new List<FindCityProvinceDto>();
            if (totalCount > 0)
            {
                var selectQuery = query.OrderBy(input.GetOrdering())
                .Select(l => new FindCityProvinceDto
                {
                     Id = l.Id,
                     Code = l.Code,
                     Name = l.Name,
                     DisplayName = l.DisplayName,
                     ISO = l.ISO,
                     CountryName = !l.CountryId.HasValue ? "" : isDefaultLanguage ? l.Country.Name : l.Country.DisplayName,
                     IsActive = l.IsActive,
                });

                if (input.UsePagination) selectQuery = selectQuery.PageBy(input);

                items = await selectQuery.ToListAsync();
            }

            return new PagedResultDto<FindCityProvinceDto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_CityProvinces_View, PermissionNames.Pages_Setup_Locations_CityProvinces_Edit)]
        public async Task<CityProvinceDetailDto> GetDetail(EntityDto<Guid> input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _cityProvinceRepository.GetAll()
                        .AsNoTracking()
                        .Where(s => s.Id == input.Id)
                        .Select(l => new CityProvinceDetailDto
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
                            CountryName = !l.CountryId.HasValue ? "" : isDefaultLanguage ? l.Country.Name : l.Country.DisplayName,
                            ISO = l.ISO
                        });

            var result = await query.FirstOrDefaultAsync();
            if (result == null) throw new UserFriendlyException(L("RecordNotFound"));

            await _cityProvinceManager.MapNavigation(result);

            return result;
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_CityProvinces)]
        public async Task<PagedResultDto<CityProvinceListDto>> GetList(PageCityProvinceInputDto input)
        {
            return await GetListHelper(input);
        }

        private async Task<PagedResultDto<CityProvinceListDto>> GetListHelper(PageCityProvinceInputDto input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _cityProvinceRepository.GetAll()
                        .AsNoTracking()
                        .WhereIf(input.IsActive.HasValue, s => input.IsActive.Value)
                        .WhereIf(input.Countries != null && input.Countries.Ids != null && input.Countries.Ids.Any(), s =>
                            (input.Countries.Exclude && (!s.CountryId.HasValue || !input.Countries.Ids.Contains(s.CountryId.Value))) ||
                            (!input.Countries.Exclude && input.Countries.Ids.Contains(s.CountryId.Value)))
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
            var items = new List<CityProvinceListDto>();
            if (totalCount > 0)
            {
                var selectQuery = query.OrderBy(input.GetOrdering())
                .Select(l => new CityProvinceListDto
                {
                    Id = l.Id,
                    No = l.No,
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
                    ISO = l.ISO,
                    CountryName = !l.CountryId.HasValue ? "" : isDefaultLanguage ? l.Country.Name : l.Country.DisplayName,
                    Code = l.Code,
                });

                if (input.UsePagination) selectQuery = selectQuery.PageBy(input);

                items = await selectQuery.ToListAsync();
            }

            return new PagedResultDto<CityProvinceListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_CityProvinces_ExportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcel(ExportExcelCityProvinceInputDto input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
          
            PagedResultDto<CityProvinceListDto> listResult;
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    listResult = await GetListHelper(input);
                }
            }

            var excelInput = new ExportDataFileInput
            {
                FileName = "CityProvince.xlsx",
                Items = listResult.Items,
                Columns = input.Columns
            };

            return await _excelManager.ExportExcelAsync(excelInput);

        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_CityProvinces_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcelTemplate()
        {
            return await _cityProvinceManager.ExportExcelTemplateAsync();
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_CityProvinces_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task ImportExcel(FileTokenInput input)
        {
            var entity = MapEntity<ImportExcelEntity<Guid>, Guid>(input);
          
            CheckErrors(await _cityProvinceManager.ImportExcelAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_CityProvinces_Edit)]
        public async Task Update(CreateUpdateCityProvinceInputDto input)
        {
            var entity = MapEntity<CityProvince, Guid>(input);

            CheckErrors(await _cityProvinceManager.UpdateAsync(entity));
        }
    }
}
