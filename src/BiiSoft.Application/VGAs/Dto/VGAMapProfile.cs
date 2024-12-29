using AutoMapper;
using BiiSoft.Items;

namespace BiiSoft.VGAs.Dto
{
    public class VGAMapProfile : Profile
    {
        public VGAMapProfile()
        {
            CreateMap<CreateUpdateVGAInputDto, VGA>().ReverseMap();
            CreateMap<VGADetailDto, VGA>().ReverseMap();
            CreateMap<FindVGADto, VGA>().ReverseMap();
        }
    }
}
