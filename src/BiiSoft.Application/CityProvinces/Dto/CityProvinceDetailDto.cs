using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.CityProvinces.Dto
{
    public class CityProvinceDetailDto : CanModifyNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public string Code { get; set; }
        public string ISO { get; set; }       

        public Guid? FirstId { get; set; }
        public Guid? NextId { get; set; }
        public Guid? PreviousId { get; set; }
        public Guid? LastId { get; set; }
        public Guid? CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
