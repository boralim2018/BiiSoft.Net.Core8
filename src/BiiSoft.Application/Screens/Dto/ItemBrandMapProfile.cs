using AutoMapper;
using BiiSoft.Items;

namespace BiiSoft.Screens.Dto
{
    public class ScreenMapProfile : Profile
    {
        public ScreenMapProfile()
        {
            CreateMap<CreateUpdateScreenInputDto, Screen>().ReverseMap();
            CreateMap<ScreenDetailDto, Screen>().ReverseMap();
            CreateMap<FindScreenDto, Screen>().ReverseMap();
        }
    }
}
