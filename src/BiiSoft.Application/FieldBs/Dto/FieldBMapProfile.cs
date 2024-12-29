using AutoMapper;
using BiiSoft.Items;

namespace BiiSoft.FieldBs.Dto
{
    public class FieldBMapProfile : Profile
    {
        public FieldBMapProfile()
        {
            CreateMap<CreateUpdateFieldBInputDto, FieldB>().ReverseMap();
            CreateMap<FieldBDetailDto, FieldB>().ReverseMap();
            CreateMap<FindFieldBDto, FieldB>().ReverseMap();
        }
    }
}
