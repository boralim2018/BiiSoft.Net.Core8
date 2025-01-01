using BiiSoft.Enums;
using System;

namespace BiiSoft.Sessions.Dto
{
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

}
