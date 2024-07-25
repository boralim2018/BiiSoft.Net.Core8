using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Villages.Dto
{
    public class FindVillageDto : NameActiveDto<Guid>
    {      
        public string Code { get; set; }
        public string CountryName { get; set; }
        public string CityProvinceName { get; set; }
        public string KhanDistrictName { get; set; }
        public string SangkatCommuneName { get; set; }
    }
}
