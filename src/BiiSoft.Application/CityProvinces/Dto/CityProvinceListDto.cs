using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.CityProvinces.Dto
{
    public class CityProvinceListDto : CanModifyNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public string Code { get; set; }
        public string ISO { get; set; }
        public string CountryName { get; set; }
    }
}
