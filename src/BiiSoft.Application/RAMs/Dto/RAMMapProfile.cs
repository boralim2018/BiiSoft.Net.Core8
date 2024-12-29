using AutoMapper;
using BiiSoft.Items;

namespace BiiSoft.RAMs.Dto
{
    public class RAMMapProfile : Profile
    {
        public RAMMapProfile()
        {
            CreateMap<CreateUpdateRAMInputDto, RAM>().ReverseMap();
            CreateMap<RAMDetailDto, RAM>().ReverseMap();
            CreateMap<FindRAMDto, RAM>().ReverseMap();
        }
    }
}
