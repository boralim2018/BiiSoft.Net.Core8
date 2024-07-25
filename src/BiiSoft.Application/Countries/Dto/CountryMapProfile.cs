using AutoMapper;
using BiiSoft.Locations;

namespace BiiSoft.Countries.Dto
{
    public class CountryMapProfile : Profile
    {
        public CountryMapProfile()
        {
            CreateMap<CreateUpdateCountryInputDto, Country>().ReverseMap();
            CreateMap<CountryDetailDto, Country>().ReverseMap();
        }
    }
}
