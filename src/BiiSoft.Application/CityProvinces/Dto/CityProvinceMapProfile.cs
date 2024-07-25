using AutoMapper;
using BiiSoft.Locations;

namespace BiiSoft.CityProvinces.Dto
{
    public class CityProvinceMapProfile : Profile
    {
        public CityProvinceMapProfile()
        {
            CreateMap<CreateUpdateCityProvinceInputDto, CityProvince>().ReverseMap();
            CreateMap<CityProvinceDetailDto, CityProvince>().ReverseMap();
        }
    }
}
