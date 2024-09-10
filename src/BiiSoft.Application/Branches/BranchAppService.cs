using Abp.Application.Services.Dto;
using Abp.Authorization;
using BiiSoft.Authorization;
using BiiSoft.BFiles;
using BiiSoft.Branches.Dto;
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
using BiiSoft.ContactInfo;
using BiiSoft.ContactInfo.Dto;
using BiiSoft.Entities;

namespace BiiSoft.Branches
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class BranchAppService : BiiSoftExcelAppServiceBase, IBranchAppService
    {
        private readonly IBranchManager _branchManager;
        private readonly IBiiSoftRepository<Branch, Guid> _branchRepository;
        private readonly IContactAddressManager _contactAddressManager;
        private readonly IBiiSoftRepository<ContactAddress, Guid> _contactAddressRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public BranchAppService(
            IUnitOfWorkManager unitOfWorkManager,
            IBranchManager branchManager,
            IBiiSoftRepository<Branch, Guid> branchRepository,
            IContactAddressManager contactAddressManager,
            IBiiSoftRepository<ContactAddress, Guid> contactAddressRepository,
            IBiiSoftRepository<User, long> userRepository)
        {
            _branchManager=branchManager;
            _branchRepository=branchRepository;
            _contactAddressManager=contactAddressManager;
            _contactAddressRepository=contactAddressRepository;
            _userRepository=userRepository;
            _unitOfWorkManager=unitOfWorkManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Company_Branches_Create)]
        public async Task<Guid> Create(CreateUpdateBranchInputDto input)
        {   
            var entity = MapEntity<Branch, Guid>(input); 

            CheckErrors(await _branchManager.InsertAsync(entity));

            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Company_Branches_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            CheckErrors(await _branchManager.DeleteAsync(input.Id));
        }

        [AbpAuthorize(PermissionNames.Pages_Company_Branches_Disable)]
        public async Task Disable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _branchManager.DisableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Company_Branches_Enable)]
        public async Task Enable(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _branchManager.EnableAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Company_Branches_SetAsDefault)]
        public async Task SetAsDefault(EntityDto<Guid> input)
        {
            var entity = MapEntity<UserEntity<Guid>, Guid>(input);

            CheckErrors(await _branchManager.SetAsDefaultAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Find_Branches)]
        public async Task<PagedResultDto<FindBranchDto>> Find(PageBranchInputDto input)
        {
            var query = _branchRepository.GetAll()
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
                            s.DisplayName.ToLower().Contains(input.Keyword.ToLower()));
                       

            var totalCount = await query.CountAsync();
            var items = new List<FindBranchDto>();
            if (totalCount > 0)
            {
                var selectQuery = query.OrderBy(input.GetOrdering())
                .Select(l => new FindBranchDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    BusinessId = l.BusinessId,
                    PhoneNumber = l.PhoneNumber,
                    IsActive = l.IsActive,
                    IsDefault = l.IsDefault
                 });

                if (input.UsePagination) selectQuery = selectQuery.PageBy(input);

                items = await selectQuery.ToListAsync();
            }

            return new PagedResultDto<FindBranchDto> { TotalCount = totalCount, Items = items };
        }

        [AbpAuthorize(PermissionNames.Pages_Company_Branches_View, PermissionNames.Pages_Company_Branches_Edit)]
        public async Task<BranchDetailDto> GetDetail(EntityDto<Guid> input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _branchRepository.GetAll()
                        .AsNoTracking()
                        .Where(s => s.Id == input.Id)
                        .Select(l => new BranchDetailDto
                        {
                            Id = l.Id,
                            No = l.No,
                            Name = l.Name,
                            DisplayName = l.DisplayName,
                            BusinessId = l.BusinessId,
                            PhoneNumber = l.PhoneNumber,
                            Email = l.Email,
                            Website = l.Website,
                            TaxRegistrationNumber = l.TaxRegistrationNumber,
                            IsDefault = l.IsDefault,
                            IsActive = l.IsActive,
                            CreationTime = l.CreationTime,
                            CreatorUserId = l.CreatorUserId,
                            CreatorUserName = l.CreatorUserId.HasValue ? l.CreatorUser.UserName : "",
                            LastModifierUserId = l.LastModifierUserId,
                            LastModificationTime = l.LastModificationTime,
                            LastModifierUserName = l.LastModifierUserId.HasValue ? l.LastModifierUser.UserName : "",

                            BillingAddress = new ContactAddressDto
                            {
                                Id = l.BillingAddressId,
                                CountryId = l.BillingAddress.CountryId,
                                CityProvinceId = l.BillingAddress.CityProvinceId,
                                KhanDistrictId = l.BillingAddress.KhanDistrictId,
                                SangkatCommuneId = l.BillingAddress.SangkatCommuneId,
                                VillageId = l.BillingAddress.VillageId,
                                LocationId = l.BillingAddress.LocationId,
                                CountryName = !l.BillingAddress.CountryId.HasValue ? "" : isDefaultLanguage ? l.BillingAddress.Country.Name : l.BillingAddress.Country.DisplayName,
                                CityProvinceName = !l.BillingAddress.CityProvinceId.HasValue ? "" : isDefaultLanguage ? l.BillingAddress.CityProvince.Name : l.BillingAddress.CityProvince.DisplayName,
                                KhanDistrictName = !l.BillingAddress.KhanDistrictId.HasValue ? "" : isDefaultLanguage ? l.BillingAddress.KhanDistrict.Name : l.BillingAddress.KhanDistrict.DisplayName,
                                SangkatCommuneName = !l.BillingAddress.SangkatCommuneId.HasValue ? "" : isDefaultLanguage ? l.BillingAddress.SangkatCommune.Name : l.BillingAddress.SangkatCommune.DisplayName,
                                VillageName = !l.BillingAddress.VillageId.HasValue ? "" : isDefaultLanguage ? l.BillingAddress.Village.Name : l.BillingAddress.Village.DisplayName,
                                LocationName = !l.BillingAddress.LocationId.HasValue ? "" : isDefaultLanguage ? l.BillingAddress.Location.Name : l.BillingAddress.Location.DisplayName,
                                PostalCode = l.BillingAddress.PostalCode,
                                Street = l.BillingAddress.Street,
                                HouseNo = l.BillingAddress.HouseNo
                            },
                            SameAsBillingAddress = l.SameAsBillingAddress,
                            ShippingAddress = new ContactAddressDto
                            {
                                Id = l.ShippingAddressId,
                                CountryId = l.ShippingAddress.CountryId,
                                CityProvinceId = l.ShippingAddress.CityProvinceId,
                                KhanDistrictId = l.ShippingAddress.KhanDistrictId,
                                SangkatCommuneId = l.ShippingAddress.SangkatCommuneId,
                                VillageId = l.ShippingAddress.VillageId,
                                LocationId = l.ShippingAddress.LocationId,
                                CountryName = !l.ShippingAddress.CountryId.HasValue ? "" : isDefaultLanguage ? l.ShippingAddress.Country.Name : l.ShippingAddress.Country.DisplayName,
                                CityProvinceName = !l.ShippingAddress.CityProvinceId.HasValue ? "" : isDefaultLanguage ? l.ShippingAddress.CityProvince.Name : l.ShippingAddress.CityProvince.DisplayName,
                                KhanDistrictName = !l.ShippingAddress.KhanDistrictId.HasValue ? "" : isDefaultLanguage ? l.ShippingAddress.KhanDistrict.Name : l.ShippingAddress.KhanDistrict.DisplayName,
                                SangkatCommuneName = !l.ShippingAddress.SangkatCommuneId.HasValue ? "" : isDefaultLanguage ? l.ShippingAddress.SangkatCommune.Name : l.ShippingAddress.SangkatCommune.DisplayName,
                                VillageName = !l.ShippingAddress.VillageId.HasValue ? "" : isDefaultLanguage ? l.ShippingAddress.Village.Name : l.ShippingAddress.Village.DisplayName,
                                LocationName = !l.ShippingAddress.LocationId.HasValue ? "" : isDefaultLanguage ? l.ShippingAddress.Location.Name : l.ShippingAddress.Location.DisplayName,
                                PostalCode = l.ShippingAddress.PostalCode,
                                Street = l.ShippingAddress.Street,
                                HouseNo = l.ShippingAddress.HouseNo
                            }

                        });

            var result = await query.FirstOrDefaultAsync();
            if (result == null) throw new UserFriendlyException(L("RecordNotFound"));

            await _branchManager.MapNavigation(result);

            return result;
        }


        [AbpAuthorize(PermissionNames.Pages_Company_Branches)]
        public async Task<PagedResultDto<BranchListDto>> GetList(PageBranchInputDto input)
        {
            return await GetListHelper(input);
        }

        private async Task<PagedResultDto<BranchListDto>> GetListHelper(PageBranchInputDto input)
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var query = _branchRepository.GetAll()
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
                            s.DisplayName.ToLower().Contains(input.Keyword.ToLower()));
                      

            var totalCount = await query.CountAsync();
            var items = new List<BranchListDto>();
            if (totalCount > 0)
            {
                var selectQuery = query.OrderBy(input.GetOrdering())
                .Select(l => new BranchListDto
                {
                    Id = l.Id,
                    No = l.No,
                    Name = l.Name,
                    DisplayName = l.DisplayName,
                    BusinessId = l.BusinessId,
                    PhoneNumber = l.PhoneNumber,
                    Email = l.Email,
                    Website = l.Website,
                    TaxRegistrationNumber = l.TaxRegistrationNumber,
                    IsDefault = l.IsDefault,
                    IsActive = l.IsActive,
                    CreationTime = l.CreationTime,
                    CreatorUserId = l.CreatorUserId,
                    CreatorUserName = l.CreatorUserId.HasValue ? l.CreatorUser.UserName : "",
                    LastModifierUserId = l.LastModifierUserId,
                    LastModificationTime = l.LastModificationTime,
                    LastModifierUserName = l.LastModifierUserId.HasValue ? l.LastModifierUser.UserName : ""            
                });

                if (input.UsePagination) selectQuery = selectQuery.PageBy(input);

                items = await selectQuery.ToListAsync();
            }

            return new PagedResultDto<BranchListDto> { TotalCount = totalCount, Items = items };
        }


        [AbpAuthorize(PermissionNames.Pages_Company_Branches_ExportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcel(ExportExcelBranchInputDto input)
        {
            if (input.Columns == null || !input.Columns.Any(s => s.Visible)) throw new UserFriendlyException(L("ColumnsIsRequired", L("ExportExcel")));

            input.UsePagination = false;
          
            PagedResultDto<BranchListDto> listResult;
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
                {
                    listResult = await GetListHelper(input);
                }
            }

            var excelInput = new ExportFileInput
            {
                FileName = "Branch.xlsx",
                Items = listResult.Items,
                Columns = input.Columns
            };

            return await ExportExcelAsync(excelInput);

        }

        [AbpAuthorize(PermissionNames.Pages_Company_Branches_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ExportFileOutput> ExportExcelTemplate()
        {
            return await _branchManager.ExportExcelTemplateAsync();
        }

        [AbpAuthorize(PermissionNames.Pages_Company_Branches_ImportExcel)]
        [UnitOfWork(IsDisabled = true)]
        public async Task ImportExcel(FileTokenInput input)
        {
            var entity = MapEntity<ImportExcelEntity<Guid>, Guid>(input);

            CheckErrors(await _branchManager.ImportExcelAsync(entity));
        }

        [AbpAuthorize(PermissionNames.Pages_Company_Branches_Edit)]
        public async Task Update(CreateUpdateBranchInputDto input)
        {
            var entity = MapEntity<Branch, Guid>(input);

            CheckErrors(await _branchManager.UpdateAsync(entity));          
        }

    }
}
