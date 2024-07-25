using AutoMapper;

namespace BiiSoft.Locations.Dto
{
    public class LocationMapProfile : Profile
    {
        public LocationMapProfile()
        {
            CreateMap<CreateUpdateLocationInputDto, Location>().ReverseMap();
            CreateMap<LocationDetailDto, Location>().ReverseMap();
        }
    }
}
