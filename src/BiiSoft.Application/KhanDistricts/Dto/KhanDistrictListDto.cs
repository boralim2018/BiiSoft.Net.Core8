using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.KhanDistricts.Dto
{
    public class KhanDistrictListDto : CanModifyNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public string Code { get; set; }
        public string CountryName { get; set; }
        public string CityProvinceName { get; set; }
    }
}
