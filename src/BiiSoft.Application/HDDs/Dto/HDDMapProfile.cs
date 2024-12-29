using AutoMapper;
using BiiSoft.Items;

namespace BiiSoft.HDDs.Dto
{
    public class HDDMapProfile : Profile
    {
        public HDDMapProfile()
        {
            CreateMap<CreateUpdateHDDInputDto, HDD>().ReverseMap();
            CreateMap<HDDDetailDto, HDD>().ReverseMap();
            CreateMap<FindHDDDto, HDD>().ReverseMap();
        }
    }
}
