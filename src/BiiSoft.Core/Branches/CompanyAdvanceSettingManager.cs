using BiiSoft.Currencies;
using BiiSoft.Locations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Branches
{
    public class CompanyAdvanceSettingManager : BiiSoftValidateServiceBase<CompanyAdvanceSetting, long>, ICompanyAdvanceSettingManager
    {
        private readonly IBiiSoftRepository<Country, Guid> _countryRepository;
        private readonly IBiiSoftRepository<Currency, long> _currencyRepository;
        public CompanyAdvanceSettingManager(
            IBiiSoftRepository<Country, Guid> countryRepository,
            IBiiSoftRepository<Currency, long> currencyRepository,
            IBiiSoftRepository<CompanyAdvanceSetting, long> repository) 
        : base(repository)
        {
            _countryRepository = countryRepository;
            _currencyRepository = currencyRepository;
        }

        protected override string InstanceName => L("OtherSetting");

        protected override CompanyAdvanceSetting CreateInstance(CompanyAdvanceSetting input)
        {
            return CompanyAdvanceSetting.Create(input.TenantId, input.CreatorUserId, input.MultiBranchesEnable, input.MultiCurrencyEnable, input.LineDiscountEnable, input.TotalDiscountEnable, input.ClassEnable, input.ContactAddressLevel);
        }

        protected override void UpdateInstance(CompanyAdvanceSetting input, CompanyAdvanceSetting entity)
        {
            entity.Update(input.LastModifierUserId, input.MultiBranchesEnable, input.MultiCurrencyEnable, input.LineDiscountEnable, input.TotalDiscountEnable, input.ClassEnable, input.ContactAddressLevel);
        }

        protected override async Task ValidateInputAsync(CompanyAdvanceSetting input)
        {
            await Task.Run(() => { });
        }

    }
}
