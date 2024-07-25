using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Locations.Dto
{
    public class FindLocationDto : NameActiveDto<Guid>
    {      
        public string Code { get; set; }
    }
}
