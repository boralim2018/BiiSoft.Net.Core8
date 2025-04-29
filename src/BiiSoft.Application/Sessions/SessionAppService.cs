using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Domain.Repositories;
using BiiSoft.Branches;
using BiiSoft.CompanySettings.Dto;
using BiiSoft.Items;
using BiiSoft.Items.Dto;
using BiiSoft.Sessions.Dto;
using Microsoft.EntityFrameworkCore;

namespace BiiSoft.Sessions
{
    public class SessionAppService : BiiSoftAppServiceBase, ISessionAppService
    {
        private readonly IRepository<CompanyGeneralSetting, long> _companyGeneralSettingRepository;
        private readonly IRepository<CompanyAdvanceSetting, long> _companyAdvanceSettingRepository;
        private readonly IBiiSoftRepository<ItemSetting, Guid> _itemSettingRepository;

        public SessionAppService(
            IBiiSoftRepository<ItemSetting, Guid> itemSettingRepository,
            IRepository<CompanyGeneralSetting, long> companyGeneralSettingRepository,
            IRepository<CompanyAdvanceSetting, long> companyAdvanceSettingRepository)
        {
            _itemSettingRepository = itemSettingRepository;
            _companyGeneralSettingRepository = companyGeneralSettingRepository;
            _companyAdvanceSettingRepository = companyAdvanceSettingRepository;
        }

        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                Application = new ApplicationInfoDto
                {
                    Version = AppVersionHelper.Version,
                    ReleaseDate = AppVersionHelper.ReleaseDate,
                    Features = new Dictionary<string, bool>()
                }
            };

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = ObjectMapper.Map<TenantLoginInfoDto>(await GetCurrentTenantAsync());

                var isDefaultLanguage = await IsDefaultLagnuageAsync();

                output.GeneralSetting = await _companyGeneralSettingRepository.GetAll()
                                           .AsNoTracking()
                                           .Select(s => new CompanyGeneralSettingDto
                                           {
                                               Id = s.Id,
                                               CountryId = s.CountryId,
                                               CountryName = !s.CountryId.HasValue ? "" : isDefaultLanguage ? s.Country.Name : s.Country.DisplayName,
                                               DefaultTimeZone = s.DefaultTimeZone,
                                               CurrencyId = s.CurrencyId,
                                               CurrencyCode = s.CurrencyId.HasValue ? s.Currency.Code : "",
                                               BusinessStartDate = s.BusinessStartDate,
                                               RoundCostDigits = s.RoundCostDigits,
                                               RoundTotalDigits = s.RoundTotalDigits,
                                               ContactAddressLevel = s.ContactAddressLevel
                                           })
                                           .FirstOrDefaultAsync();

                output.AdvanceSetting = await _companyAdvanceSettingRepository.GetAll()
                                           .AsNoTracking()
                                           .Select(s => new CompanyAdvanceSettingDto
                                           {
                                               Id = s.Id,
                                               MultiBranchesEnable = s.MultiBranchesEnable,
                                               MultiCurrencyEnable = s.MultiCurrencyEnable,
                                               LineDiscountEnable = s.LineDiscountEnable,
                                               TotalDiscountEnable = s.TotalDiscountEnable,
                                               CustomAccountCodeEnable = s.CustomAccountCodeEnable,
                                               ClassEnable = s.ClassEnable,
                                               TaxEnable = s.TaxEnable
                                           })
                                           .FirstOrDefaultAsync();

                var setting = await _itemSettingRepository.GetAll()
                                    .Include(s => s.InventoryAccount)
                                    .Include(s => s.AssetAccount)
                                    .Include(s => s.ExpenseAccount)
                                    .Include(s => s.COGSAccount)
                                    .Include(s => s.RevenueAccount)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync();

                output.ItemSetting = ObjectMapper.Map<ItemSettingDto>(setting);
                if (setting?.InventoryAccount != null) output.ItemSetting.InventoryAccountName = isDefaultLanguage ? setting.InventoryAccount.Name : setting.InventoryAccount.DisplayName;
                if (setting?.AssetAccount != null) output.ItemSetting.AssetAccountName = isDefaultLanguage ? setting.AssetAccount.Name : setting.AssetAccount.DisplayName;
                if (setting?.ExpenseAccount != null) output.ItemSetting.ExpenseAccountName = isDefaultLanguage ? setting.ExpenseAccount.Name : setting.ExpenseAccount.DisplayName;
                if (setting?.COGSAccount != null) output.ItemSetting.COGSAccountName = isDefaultLanguage ? setting.COGSAccount.Name : setting.COGSAccount.DisplayName;
                if (setting?.RevenueAccount != null) output.ItemSetting.RevenueAccountName = isDefaultLanguage ? setting.RevenueAccount.Name : setting.RevenueAccount.DisplayName;

            }

            if (AbpSession.UserId.HasValue)
            {
                output.User = ObjectMapper.Map<UserLoginInfoDto>(await GetCurrentUserAsync());
            }

            return output;
        }
    }
}
