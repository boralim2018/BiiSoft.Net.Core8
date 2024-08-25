using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Domain.Repositories;
using BiiSoft.Branches;
using BiiSoft.Sessions.Dto;
using Microsoft.EntityFrameworkCore;

namespace BiiSoft.Sessions
{
    public class SessionAppService : BiiSoftAppServiceBase, ISessionAppService
    {
        private readonly IRepository<CompanyGeneralSetting, long> _companyGeneralSettingRepository;
        private readonly IRepository<CompanyAdvanceSetting, long> _companyAdvanceSettingRepository;

        public SessionAppService(
            IRepository<CompanyGeneralSetting, long> companyGeneralSettingRepository,
            IRepository<CompanyAdvanceSetting, long> companyAdvanceSettingRepository)
        {
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
                                           .Select(s => new GeneralSettingDto
                                           {
                                               CountryId = s.CountryId,
                                               CountryName = !s.CountryId.HasValue ? "" : isDefaultLanguage ? s.Country.Name : s.Country.DisplayName,
                                               DefaultTimeZone = s.DefaultTimeZone,
                                               CurrencyId = s.CurrencyId,
                                               CurrencyCode = s.CurrencyId.HasValue ? s.Currency.Code : "",
                                               BusinessStartDate = s.BusinessStartDate,
                                               RoundCostDigits = s.RoundCostDigits,
                                               RoundTotalDigits = s.RoundTotalDigits
                                           })
                                           .FirstAsync();

                output.AdvanceSetting = await _companyAdvanceSettingRepository.GetAll()
                                           .AsNoTracking()
                                           .Select(s => new AdvanceSettingDto
                                           {
                                               MultiBranchesEnable = s.MultiBranchesEnable,
                                               MultiCurrencyEnable = s.MultiCurrencyEnable,
                                               LineDiscountEnable = s.LineDiscountEnable,
                                               TotalDiscountEnable = s.TotalDiscountEnable,
                                               ClassEnable = s.ClassEnable,
                                               ContactAddressLevel = s.ContactAddressLevel
                                           })
                                           .FirstAsync();

            }

            if (AbpSession.UserId.HasValue)
            {
                output.User = ObjectMapper.Map<UserLoginInfoDto>(await GetCurrentUserAsync());
            }

            return output;
        }
    }
}
