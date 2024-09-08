using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Countries.Dto
{
    public class CountryDetailDto : CanModifyNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
        public string Code { get; set; }
        public string ISO { get; set; }
        public string ISO2 { get; set; }
        public string PhonePrefix { get; set; }
        public long? CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
    }
}
