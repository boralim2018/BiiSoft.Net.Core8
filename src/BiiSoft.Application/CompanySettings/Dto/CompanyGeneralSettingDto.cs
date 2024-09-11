using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.CompanySettings.Dto
{
    public class CompanyGeneralSettingDto
    {
        public long? Id { get; set; }
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
