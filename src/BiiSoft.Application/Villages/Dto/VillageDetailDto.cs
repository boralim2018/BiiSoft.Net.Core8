using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Villages.Dto
{
    public class VillageDetailDto : CanModifyNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public string Code { get; set; }
        public Guid? CountryId { get; set; }
        public string CountryName { get; set; }
        public Guid? CityProvinceId { get; set; }
        public string CityProvinceName { get; set; }
        public Guid? KhanDistrictId { get; set; }
        public string KhanDistrictName { get; set; }
        public Guid? SangkatCommuneId { get; set; }
        public string SangkatCommuneName { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public Guid? FirstId { get; set; }
        public Guid? NextId { get; set; }
        public Guid? PreviousId { get; set; }
        public Guid? LastId { get; set; }
    }
}
