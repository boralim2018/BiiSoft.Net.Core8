using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.CityProvinces.Dto
{
    public class CityProvinceDetailDto : CanModifyNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
        public string Code { get; set; }
        public string ISO { get; set; }       

        public Guid? CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
