using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Countries.Dto
{
    public class CountryListDto : CanModifyNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public string Code { get; set; }
        public int CountryCode { get; set; }
        public string ISO { get; set; }
        public string ISO2 { get; set; }
        public string PhonePrefix { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string CurrencyCode { get; set; }
    }
}
