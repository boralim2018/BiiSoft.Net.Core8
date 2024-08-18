using BiiSoft.Currencies;
using BiiSoft.Locations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Branches
{
    public class CompanyGeneralSettingManager : BiiSoftValidateServiceBase<CompanyGeneralSetting, long>, ICompanyGeneralSettingManager
    {
        private readonly IBiiSoftRepository<Country, Guid> _countryRepository;
        private readonly IBiiSoftRepository<Currency, long> _currencyRepository;
        public CompanyGeneralSettingManager(
            IBiiSoftRepository<Country, Guid> countryRepository,
            IBiiSoftRepository<Currency, long> currencyRepository,
            IBiiSoftRepository<CompanyGeneralSetting, long> repository) 
        : base(repository)
        {
            _countryRepository = countryRepository;
            _currencyRepository = currencyRepository;
        }

        protected override string InstanceName => L("GeneralSetting");

        protected override CompanyGeneralSetting CreateInstance(CompanyGeneralSetting input)
        {
            return CompanyGeneralSetting.Create(input.TenantId, input.CreatorUserId, input.CountryId, input.DefaultTimeZone, input.CurrencyId, input.BusinessStartDate, input.RoundTotalDigits, input.RoundCostDigits);
        }

        protected override void UpdateInstance(CompanyGeneralSetting input, CompanyGeneralSetting entity)
        {
            entity.Update(input.LastModifierUserId, input.CountryId, input.DefaultTimeZone, input.CurrencyId, input.BusinessStartDate, input.RoundTotalDigits, input.RoundCostDigits);
        }

        protected override async Task ValidateInputAsync(CompanyGeneralSetting input)
        {
            ValidateSelect(input.CountryId, L("Country"));
            ValidateSelect(input.CurrencyId, L("Currency"));
            ValidateSelect(input.DefaultTimeZone, L("Timezone"));
            ValidateSelect(input.BusinessStartDate, L("BusinessStartDate"));
            ValidateSelect(input.RoundTotalDigits, L("Rounding_", L("Total")));
            ValidateSelect(input.RoundCostDigits, L("Rounding_", L("Cost")));
           
            var findCountry = await _countryRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.CountryId.Value);
            if (!findCountry) InvalidException(L("Country"));
            
            var findCurrency = await _currencyRepository.GetAll().AsNoTracking().AnyAsync(s => s.Id == input.CurrencyId.Value);
            if (!findCurrency) InvalidException(L("Currency"));
        }

    }
}
