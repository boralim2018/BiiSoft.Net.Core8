using AutoMapper;
using BiiSoft.Items;

namespace BiiSoft.ColorPatterns.Dto
{
    public class ColorPatternMapProfile : Profile
    {
        public ColorPatternMapProfile()
        {
            CreateMap<CreateUpdateColorPatternInputDto, ColorPattern>().ReverseMap();
            CreateMap<ColorPatternDetailDto, ColorPattern>().ReverseMap();
            CreateMap<FindColorPatternDto, ColorPattern>().ReverseMap();
        }
    }
}
