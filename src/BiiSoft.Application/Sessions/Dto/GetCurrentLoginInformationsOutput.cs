using BiiSoft.Enums;
using System;

namespace BiiSoft.Sessions.Dto
{
    public class GetCurrentLoginInformationsOutput
    {
        public ApplicationInfoDto Application { get; set; }
        public UserLoginInfoDto User { get; set; }
        public TenantLoginInfoDto Tenant { get; set; }
        public GeneralSettingDto GeneralSetting { get; set; }
        public AdvanceSettingDto AdvanceSetting { get; set; }
    }

    public class GeneralSettingDto
    {
        public Guid? CountryId { get; set; }
        public string CountryName { get; set; }
        public string DefaultTimeZone { get; set; }
        public long? CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime? BusinessStartDate { get; set; }

        public int RoundTotalDigits { get; set; }
        public int RoundCostDigits { get; set; }
        public AddressLevel ContactAddressLevel { get; set; }
    }

    public class AdvanceSettingDto
    {
        public bool MultiBranchesEnable { get; set; }
        public bool MultiCurrencyEnable { get; set; }
        public bool LineDiscountEnable { get; set; }
        public bool TotalDiscountEnable { get; set; }
        public bool ClassEnable { get; set; }
    }
}
