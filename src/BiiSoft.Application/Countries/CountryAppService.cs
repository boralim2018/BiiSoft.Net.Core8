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
using BiiSoft.Countries.Dto;
using BiiSoft.Entities;
using BiiSoft.BFiles.Dto;

namespace BiiSoft.Countries
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class CountryAppService : BiiSoftExcelAppServiceBase, ICountryAppService
    {
        private readonly ICountryManager _countryManager;
        private readonly IBiiSoftRepository<Country, Guid> _countryRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        
        public CountryAppService(
            IUnitOfWorkManager unitOfWorkManager,
            ICountryManager countryManager,
            IBiiSoftRepository<Country, Guid> countryRepository,
            IBiiSoftRepository<User, long> userRepository)
        {
            _countryManager=countryManager;
            _countryRepository=countryRepository;
            _userRepository=userRepository;
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
            var query = _countryRepository.GetAll()
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
                            s.DisplayName.ToLower().Contains(input.Keyword.ToLower()));

            var totalCount = await query.CountAsync();
            var items = new List<FindCountryDto>();
            if (totalCount > 0)
            {
                var selectQuery = query.OrderBy(input.GetOrdering())
                .Select(l => new FindCountryDto
                {
                    Id = l.Id,
                    Code = l.Code,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    ISO = l.ISO,
                    ISO2 = l.ISO2,
                    PhonePrefix = l.PhonePrefix,
                    CurrencyId = l.CurrencyId,
                    CurrencyCode = l.CurrencyId.HasValue ? l.Currency.Code : "",
                    IsActive = l.IsActive,
                });

                if (input.UsePagination) selectQuery = selectQuery.PageBy(input);

                items = await selectQuery.ToListAsync();
            }

            return new PagedResultDto<FindCountryDto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Countries_View, PermissionNames.Pages_Setup_Locations_Countries_Edit)]
        public async Task<CountryDetailDto> GetDetail(EntityDto<Guid> input)
        {
            var query = _countryRepository.GetAll()
                        .AsNoTracking()
                        .Where(s => s.Id == input.Id)
                        .Select(l => new CountryDetailDto
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
                            CurrencyId = l.CurrencyId,
                            CurrencyCode = l.CurrencyId.HasValue ? l.Currency.Code : "",
                            PhonePrefix = l.PhonePrefix,
                            ISO = l.ISO,
                            ISO2 = l.ISO2
                        });

            var result = await query.FirstOrDefaultAsync();
            if (result == null) throw new UserFriendlyException(L("RecordNotFound"));

            await _countryManager.MapNavigation(result);

            return result;
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Countries)]
        public async Task<PagedResultDto<CountryListDto>> GetList(PageCountryInputDto input)
        {
            return await GetListHelper(input);
        }

        private async Task<PagedResultDto<CountryListDto>> GetListHelper(PageCountryInputDto input)
        {
            var query = _countryRepository.GetAll()
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
                            s.DisplayName.ToLower().Contains(input.Keyword.ToLower()));
                     
            var totalCount = await query.CountAsync();
            var items = new List<CountryListDto>();
            if (totalCount > 0)
            {
                var selectQuery = query.OrderBy(input.GetOrdering())
                .Select(l => new CountryListDto
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
                    CreatorUserName = l.CreatorUserId.HasValue ? l.CreatorUser.UserName : "",
                    LastModifierUserId = l.LastModifierUserId,
                    LastModificationTime = l.LastModificationTime,
                    LastModifierUserName = l.LastModifierUserId.HasValue ? l.LastModifierUser.UserName : "",
                    ISO = l.ISO,
                    ISO2 = l.ISO2,
                    PhonePrefix = l.PhonePrefix,
                    CurrencyCode = l.CurrencyId.HasValue ? l.Currency.Code : "",

                });

                if (input.UsePagination) selectQuery = selectQuery.PageBy(input);

                items = await selectQuery.ToListAsync();
            }

            return new PagedResultDto<CountryListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Countries_ExportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcel(ExportExcelCountryInputDto input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
           
            PagedResultDto<CountryListDto> listResult;
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    listResult = await GetListHelper(input);
                }
            }

            var excelInput = new ExportFileInput
            {
                FileName = "Country.xlsx",
                Items = listResult.Items,
                Columns = input.Columns
            };

            return await ExportExcelAsync(excelInput);
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Countries_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcelTemplate()
        {
            return await _countryManager.ExportExcelTemplateAsync();
        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Countries_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task ImportExcel(FileTokenInput input)
        {
            var entity = MapEntity<ImportExcelEntity<Guid>, Guid>(input);

            CheckErrors(await _countryManager.ImportExcelAsync(entity));

        }

        [AbpAuthorize(PermissionNames.Pages_Setup_Locations_Countries_Edit)]
        public async Task Update(CreateUpdateCountryInputDto input)
        {
            var entity = MapEntity<Country, Guid>(input);

            CheckErrors(await _countryManager.UpdateAsync(entity));
        }
    }
}
