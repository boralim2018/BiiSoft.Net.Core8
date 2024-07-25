using AutoMapper;
using BiiSoft.Locations;

namespace BiiSoft.Villages.Dto
{
    public class VillageMapProfile : Profile
    {
        public VillageMapProfile()
        {
            CreateMap<CreateUpdateVillageInputDto, Village>().ReverseMap();
            CreateMap<VillageDetailDto, Village>().ReverseMap();
        }
    }
}
