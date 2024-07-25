using AutoMapper;
using BiiSoft.Locations;

namespace BiiSoft.SangkatCommunes.Dto
{
    public class SangkatCommuneMapProfile : Profile
    {
        public SangkatCommuneMapProfile()
        {
            CreateMap<CreateUpdateSangkatCommuneInputDto, SangkatCommune>().ReverseMap();
            CreateMap<SangkatCommuneDetailDto, SangkatCommune>().ReverseMap();
        }
    }
}
