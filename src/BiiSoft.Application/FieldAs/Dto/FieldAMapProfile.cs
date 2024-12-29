using AutoMapper;
using BiiSoft.Items;

namespace BiiSoft.FieldAs.Dto
{
    public class FieldAMapProfile : Profile
    {
        public FieldAMapProfile()
        {
            CreateMap<CreateUpdateFieldAInputDto, FieldA>().ReverseMap();
            CreateMap<FieldADetailDto, FieldA>().ReverseMap();
            CreateMap<FindFieldADto, FieldA>().ReverseMap();
        }
    }
}
