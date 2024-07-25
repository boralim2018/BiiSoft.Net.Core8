using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.KhanDistricts.Dto
{
    public class FindKhanDistrictDto : NameActiveDto<Guid>
    {      
        public string Code { get; set; }
        public string CountryName { get; set; }
        public string CityProvinceName { get; set; }
    }
}
