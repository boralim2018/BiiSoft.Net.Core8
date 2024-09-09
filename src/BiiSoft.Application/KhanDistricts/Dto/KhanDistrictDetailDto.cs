using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.KhanDistricts.Dto
{
    public class KhanDistrictDetailDto : CanModifyNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
        public string Code { get; set; }
        public Guid? CountryId { get; set; }
        public string CountryName { get; set; }
        public Guid? CityProvinceId { get; set; }
        public string CityProvinceName { get; set; }
    }
}
