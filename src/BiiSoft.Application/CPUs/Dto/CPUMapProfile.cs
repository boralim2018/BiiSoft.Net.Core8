using AutoMapper;
using BiiSoft.Items;

namespace BiiSoft.CPUs.Dto
{
    public class CPUMapProfile : Profile
    {
        public CPUMapProfile()
        {
            CreateMap<CreateUpdateCPUInputDto, CPU>().ReverseMap();
            CreateMap<CPUDetailDto, CPU>().ReverseMap();
            CreateMap<FindCPUDto, CPU>().ReverseMap();
        }
    }
}
