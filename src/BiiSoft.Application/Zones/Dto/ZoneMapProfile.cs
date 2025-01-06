using AutoMapper;
using BiiSoft.Warehouses;

namespace BiiSoft.Zones.Dto
{
    public class ZoneMapProfile : Profile
    {
        public ZoneMapProfile()
        {
            CreateMap<CreateUpdateZoneInputDto, Zone>().ReverseMap();
            CreateMap<ZoneDetailDto, Zone>().ReverseMap();
            CreateMap<FindZoneDto, Zone>().ReverseMap();
        }
    }
}
