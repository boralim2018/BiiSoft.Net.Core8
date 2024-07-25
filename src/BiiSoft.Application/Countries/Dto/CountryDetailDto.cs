using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Countries.Dto
{
    public class CountryDetailDto : CanModifyNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public string Code { get; set; }
        public int CountryCode { get; set; }
        public string ISO { get; set; }
        public string ISO2 { get; set; }
        public string PhonePrefix { get; set; }
        public long? CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public Guid? FirstId { get; set; }
        public Guid? NextId { get; set; }
        public Guid? PreviousId { get; set; }
        public Guid? LastId { get; set; }
    }
}
