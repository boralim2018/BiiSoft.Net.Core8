using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Countries.Dto
{
    public class FindCountryDto : NameActiveDto<Guid>
    {      
        public string Code { get; set; }
        public string ISO { get; set; }
        public string ISO2 { get; set; }
        public string PhonePrefix { get; set; }
    }
}
