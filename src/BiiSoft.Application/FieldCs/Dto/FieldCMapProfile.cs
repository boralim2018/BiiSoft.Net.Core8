using AutoMapper;
using BiiSoft.Items;

namespace BiiSoft.FieldCs.Dto
{
    public class FieldCMapProfile : Profile
    {
        public FieldCMapProfile()
        {
            CreateMap<CreateUpdateFieldCInputDto, FieldC>().ReverseMap();
            CreateMap<FieldCDetailDto, FieldC>().ReverseMap();
            CreateMap<FindFieldCDto, FieldC>().ReverseMap();
        }
    }
}
