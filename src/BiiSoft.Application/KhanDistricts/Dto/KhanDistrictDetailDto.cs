using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.KhanDistricts.Dto
{
    public class KhanDistrictDetailDto : CanModifyNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public string Code { get; set; }
        public Guid? CountryId { get; set; }
        public string CountryName { get; set; }
        public Guid? CityProvinceId { get; set; }
        public string CityProvinceName { get; set; }

        public Guid? FirstId { get; set; }
        public Guid? NextId { get; set; }
        public Guid? PreviousId { get; set; }
        public Guid? LastId { get; set; }
    }
}
