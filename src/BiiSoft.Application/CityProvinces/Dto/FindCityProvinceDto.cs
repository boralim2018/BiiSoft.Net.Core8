using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.CityProvinces.Dto
{
    public class FindCityProvinceDto : NameActiveDto<Guid>
    {      
        public string Code { get; set; }
        public string ISO { get; set; }
        public string CountryName { get; set; }
    }
}
