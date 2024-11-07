using Abp.Authorization;
using BiiSoft.Authorization;
using BiiSoft.Branches.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Abp.Extensions;
using BiiSoft.Authorization.Users;
using BiiSoft.FileStorages;
using BiiSoft.Folders;
using Abp.Domain.Uow;
using BiiSoft.ContactInfo;
using BiiSoft.ContactInfo.Dto;
using BiiSoft.CompanySettings;
using BiiSoft.CompanySettings.Dto;
using BiiSoft.Extensions;
using BiiSoft.MultiTenancy.Dto;
using BiiSoft.Entities;
using BiiSoft.Enums;
using Abp.Application.Services.Dto;
using Abp.UI;

namespace BiiSoft.Branches
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class CompanySettingAppService : BiiSoftAppServiceBase, ICompanySettingAppService
    {
        private readonly ITransactionNoSettingManager _transactionNoSettingManager;
        private readonly ICompanyGeneralSettingManager _companyGeneralSettingManager;
        private readonly ICompanyAdvanceSettingManager _companyAdvanceSettingManager;
        private readonly ICompanyAccountSettingManager _companyAccountSettingManager;
        private readonly IBiiSoftRepository<TransactionNoSetting, Guid> _transactionNoSettingRepository;
        private readonly IBiiSoftRepository<CompanyGeneralSetting, long> _companyGeneralSettingRepository;
        private readonly IBiiSoftRepository<CompanyAdvanceSetting, long> _companyAdvanceSettingRepository;
        private readonly IBiiSoftRepository<CompanyAccountSetting, long> _companyAccountSettingRepository;
        private readonly IBranchManager _branchManager;
        private readonly IBiiSoftRepository<Branch, Guid> _branchRepository;
        private readonly IContactAddressManager _contactAddressManager;
        private readonly IBiiSoftRepository<ContactAddress, Guid> _contactAddressRepository;
        private readonly IBiiSoftRepository<User, long> _userRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CompanySettingAppService(
            ITransactionNoSettingManager transactionNoSettingManager,
            ICompanyGeneralSettingManager companyGeneralSettingManager,
            ICompanyAdvanceSettingManager companyAdvanceSettingManager,
            ICompanyAccountSettingManager companyAccountSettingManager,
            IBiiSoftRepository<TransactionNoSetting, Guid> transactionNoSettingRepository,
            IBiiSoftRepository<CompanyGeneralSetting, long> companyGeneralSettingRepository,
            IBiiSoftRepository<CompanyAdvanceSetting, long> companyAdvanceSettingRepository,
            IBiiSoftRepository<CompanyAccountSetting, long> companyAccountSettingRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IFileStorageManager fileStorageManager,
            IAppFolders appFolders,
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
            _companyGeneralSettingManager=companyGeneralSettingManager;
            _companyAdvanceSettingManager=companyAdvanceSettingManager;
            _companyAccountSettingManager=companyAccountSettingManager;
            _transactionNoSettingManager = transactionNoSettingManager;
            _companyGeneralSettingRepository =companyGeneralSettingRepository;
            _companyAdvanceSettingRepository=companyAdvanceSettingRepository;
            _companyAccountSettingRepository=companyAccountSettingRepository;
            _transactionNoSettingRepository=transactionNoSettingRepository;
        }

        [AbpAuthorize(PermissionNames.Pages_Company_CompanySetting_Edit)]
        public async Task<Guid> CreateOrUpdateProfile(CreateUpdateBranchInputDto input)
        {   
            var entity = MapEntity<Branch, Guid>(input);

            if (input.Id.IsNullOrEmpty())
            {
                CheckErrors(await _branchManager.InsertAsync(entity));
            }
            else
            {
                CheckErrors(await _branchManager.UpdateAsync(entity));
            }

            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Company_CompanySetting_Edit)]
        public async Task<long> CreateOrUpdateGeneralSetting(CreateUpdateCompanyGeneralSettingInputDto input)
        {
            var entity = MapEntity<CompanyGeneralSetting, long>(input);

            if (input.Id.IsNullOrZero())
            {
                CheckErrors(await _companyGeneralSettingManager.InsertAsync(entity));
            }
            else
            {
                CheckErrors(await _companyGeneralSettingManager.UpdateAsync(entity));
            }

            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Company_CompanySetting_Edit)]
        public async Task<long> CreateOrUpdateAdvanceSetting(CreateUpdateCompanyAdvanceSettingInputDto input)
        {
            var entity = MapEntity<CompanyAdvanceSetting, long>(input);

            if (input.Id.IsNullOrZero())
            {
                CheckErrors(await _companyAdvanceSettingManager.InsertAsync(entity));
            }
            else
            {
                CheckErrors(await _companyAdvanceSettingManager.UpdateAsync(entity));
            }

            return entity.Id;
        }


        [AbpAuthorize(PermissionNames.Pages_Company_CompanySetting_Edit)]
        public async Task<long> CreateOrUpdateAccountSetting(CreateUpdateCompanyAccountSettingInputDto input)
        {
            var entity = MapEntity<CompanyAccountSetting, long>(input);

            if (input.Id.IsNullOrZero())
            {
                CheckErrors(await _companyAccountSettingManager.InsertAsync(entity));
            }
            else
            {
                CheckErrors(await _companyAccountSettingManager.UpdateAsync(entity));
            }

            return entity.Id;
        }

        [AbpAuthorize(PermissionNames.Pages_Company_CompanySetting_Edit)]
        public async Task<List<NameValueDto<JournalType>>> CreateOrUpdateTransactionNoSetting(List<CreateUpdateTransactionNoSettingInputDto> input)
        {
            if (input.IsNullOrEmpty()) throw new UserFriendlyException(L("IsRequired", L("TransactionNo")));

            var createItems = input.Where(s => s.Id.IsNullOrEmpty()).ToList();
            var updateItems = input.Where(s => !s.Id.IsNullOrEmpty()).ToList();

            var result = new List<NameValueDto<JournalType>>();

            if (createItems.Any())
            {
                var entity = new MayHaveTenantBulkInputEntity<TransactionNoSetting>
                {
                    TenantId = AbpSession.TenantId,
                    UserId = AbpSession.UserId.Value,
                    Items = ObjectMapper.Map<List<TransactionNoSetting>>(createItems)
                }; 

                CheckErrors(await _transactionNoSettingManager.BulkInsertAsync(entity));

                result.AddRange(entity.Items.Select(s => new NameValueDto<JournalType> { Name = s.Id.ToString(), Value = s.JournalType }));
            }

            if (updateItems.Any())
            {
                var entity = new MayHaveTenantBulkInputEntity<TransactionNoSetting>
                {
                    TenantId = AbpSession.TenantId,
                    UserId = AbpSession.UserId.Value,
                    Items = ObjectMapper.Map<List<TransactionNoSetting>>(updateItems)
                };

                CheckErrors(await _transactionNoSettingManager.BulkUpdateAsync(entity));
            }

            return result;
        }


        [AbpAuthorize(PermissionNames.Pages_Company_CompanySetting, PermissionNames.Pages_Company_CompanySetting_Edit)]
        public async Task<CompanySettingDto> GetDetail()
        {
            var isDefaultLanguage = await IsDefaultLagnuageAsync();

            var tenant = await GetCurrentTenantAsync();

            var branch = await _branchRepository.GetAll().AsNoTracking()
            .Where(s => s.IsDefault)
            .Select(l =>
            new BranchDetailDto
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
                LastModificationTime = l.LastModificationTime,
                LastModifierUserId = l.LastModifierUserId,

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

            })
            .FirstOrDefaultAsync();

            var generalSetting = await _companyGeneralSettingRepository.GetAll().AsNoTracking()
            .Select(s => new CompanyGeneralSettingDto
            {
                Id = s.Id,
                CountryId = s.CountryId,
                CountryName = !s.CountryId.HasValue ? "" : isDefaultLanguage ? s.Country.Name : s.Country.DisplayName,
                CurrencyId = s.CurrencyId,
                CurrencyCode = s.CurrencyId.HasValue ? s.Currency.Code : "",
                DefaultTimeZone = s.DefaultTimeZone,
                BusinessStartDate = s.BusinessStartDate,               
                RoundTotalDigits = s.RoundTotalDigits,
                RoundCostDigits = s.RoundCostDigits,
                ContactAddressLevel = s.ContactAddressLevel
            })
            .FirstOrDefaultAsync();

            var advanceSetting = await _companyAdvanceSettingRepository.GetAll().AsNoTracking()
            .Select(s => new CompanyAdvanceSettingDto
            {
                Id = s.Id,
                MultiBranchesEnable = s.MultiBranchesEnable,
                MultiCurrencyEnable = s.MultiCurrencyEnable,
                LineDiscountEnable = s.LineDiscountEnable,
                TotalDiscountEnable = s.TotalDiscountEnable,
                CustomAccountCodeEnable = s.CustomAccountCodeEnable,
                ClassEnable = s.ClassEnable
            })
            .FirstOrDefaultAsync();

            var accountSetting = await _companyAccountSettingRepository.GetAll().AsNoTracking()
            .Select(s => new CompanyAccountSettingDto
            {
                Id = s.Id,
                DefaultAPAccountId = s.DefaultAPAccountId,
                DefaultARAccountId = s.DefaultARAccountId,
                DefaultPurchaseDiscountAccountId = s.DefaultPurchaseDiscountAccountId,
                DefaultSaleDiscountAccountId = s.DefaultSaleDiscountAccountId,
                DefaultInventoryPurchaseAccountId = s.DefaultInventoryPurchaseAccountId,
                DefaultBillPaymentAccountId = s.DefaultBillPaymentAccountId,
                DefaultReceivePaymentAccountId = s.DefaultReceivePaymentAccountId,
                DefaultItemReceiptAccountId = s.DefaultItemReceiptAccountId,
                DefaultItemIssueAccountId = s.DefaultItemIssueAccountId,
                DefaultItemAdjustmentAccountId = s.DefaultItemAdjustmentAccountId,
                DefaultItemTransferAccountId = s.DefaultItemTransferAccountId,
                DefaultItemProductionAccountId = s.DefaultItemProductionAccountId,
                DefaultItemExchangeAccountId = s.DefaultItemExchangeAccountId,
                DefaultCashTransferAccountId = s.DefaultCashTransferAccountId,
                DefaultCashExchangeAccountId = s.DefaultCashExchangeAccountId,
                DefaultAPAccountName = !s.DefaultAPAccountId.HasValue ? "" : isDefaultLanguage ? s.DefaultAPAccount.Name : s.DefaultAPAccount.DisplayName,
                DefaultARAccountName = !s.DefaultARAccountId.HasValue ? "" : isDefaultLanguage ? s.DefaultARAccount.Name : s.DefaultARAccount.DisplayName,
                DefaultPurchaseDiscountAccountName = !s.DefaultPurchaseDiscountAccountId.HasValue ? "" : isDefaultLanguage ? s.DefaultPurchaseDiscountAccount.Name : s.DefaultPurchaseDiscountAccount.DisplayName,
                DefaultSaleDiscountAccountName = !s.DefaultSaleDiscountAccountId.HasValue ? "" : isDefaultLanguage ? s.DefaultSaleDiscountAccount.Name : s.DefaultSaleDiscountAccount.DisplayName,
                DefaultInventoryPurchaseAccountName = !s.DefaultInventoryPurchaseAccountId.HasValue ? "" : isDefaultLanguage ? s.DefaultInventoryPurchaseAccount.Name : s.DefaultInventoryPurchaseAccount.DisplayName,
                DefaultBillPaymentAccountName = !s.DefaultBillPaymentAccountId.HasValue ? "" : isDefaultLanguage ? s.DefaultBillPaymentAccount.Name : s.DefaultBillPaymentAccount.DisplayName,
                DefaultReceivePaymentAccountName = !s.DefaultReceivePaymentAccountId.HasValue ? "" : isDefaultLanguage ? s.DefaultReceivePaymentAccount.Name : s.DefaultReceivePaymentAccount.DisplayName,
                DefaultItemReceiptAccountName = !s.DefaultItemReceiptAccountId.HasValue ? "" : isDefaultLanguage ? s.DefaultItemReceiptAccount.Name : s.DefaultItemReceiptAccount.DisplayName,
                DefaultItemIssueAccountName = !s.DefaultItemIssueAccountId.HasValue ? "" : isDefaultLanguage ? s.DefaultItemIssueAccount.Name : s.DefaultItemIssueAccount.DisplayName,
                DefaultItemAdjustmentAccountName = !s.DefaultItemAdjustmentAccountId.HasValue ? "" : isDefaultLanguage ? s.DefaultItemAdjustmentAccount.Name : s.DefaultItemAdjustmentAccount.DisplayName,
                DefaultItemTransferAccountName = !s.DefaultItemTransferAccountId.HasValue ? "" : isDefaultLanguage ? s.DefaultItemTransferAccount.Name : s.DefaultItemTransferAccount.DisplayName,
                DefaultItemProductionAccountName = !s.DefaultItemProductionAccountId.HasValue ? "" : isDefaultLanguage ? s.DefaultItemProductionAccount.Name : s.DefaultItemProductionAccount.DisplayName,
                DefaultItemExchangeAccountName = !s.DefaultItemExchangeAccountId.HasValue ? "" : isDefaultLanguage ? s.DefaultItemExchangeAccount.Name : s.DefaultItemExchangeAccount.DisplayName,
                DefaultCashTransferAccountName = !s.DefaultCashTransferAccountId.HasValue ? "" : isDefaultLanguage ? s.DefaultCashTransferAccount.Name : s.DefaultCashTransferAccount.DisplayName,
                DefaultCashExchangeAccountName = !s.DefaultCashExchangeAccountId.HasValue ? "" : isDefaultLanguage ? s.DefaultCashExchangeAccount.Name : s.DefaultCashExchangeAccount.DisplayName,
            })
            .FirstOrDefaultAsync();

            var transactionNos = await _transactionNoSettingRepository.GetAll().AsNoTracking().Select(s => new TransactionNoSettingDto
            {
                Id = s.Id,
                JournalType = s.JournalType,
                CustomTransactionNoEnable = s.CustomTransactionNoEnable,
                Prefix = s.Prefix,
                Digits = s.Digits,
                Start = s.Start,
                RequiredReference = s.RequiredReference,
            })
            .ToListAsync();

            var transactionNoSettings = Enum.GetValues(typeof(JournalType)).Cast<JournalType>()
            .Select(j => new
            {
                JournalType = j,
                Transaction = transactionNos.FirstOrDefault(t => t.JournalType == j)
            })
            .Select(x => new TransactionNoSettingDto
            {
                Id = x.Transaction?.Id,
                JournalType = x.JournalType,
                JournalTypeName = x.JournalType.ToString(),
                CustomTransactionNoEnable = x.Transaction?.CustomTransactionNoEnable ?? false,
                Prefix = x.Transaction?.Prefix ?? "",
                Digits = x.Transaction?.Digits ?? 0,
                Start = x.Transaction?.Start ?? 0,
                RequiredReference = x.Transaction?.RequiredReference ?? false
            })
            .ToList();


            return new CompanySettingDto
            {
                CompanyLogo = new UpdateLogoInput { LogoId = tenant.LogoId },
                Branch = branch,
                GeneralSetting = generalSetting,
                AdvanceSetting = advanceSetting,
                AccountSetting = accountSetting,
                TransactionNoSettings = transactionNoSettings
            };
        }

    }
}
