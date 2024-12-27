using AutoMapper;
using BiiSoft.Items;

namespace BiiSoft.Units.Dto
{
    public class UnitMapProfile : Profile
    {
        public UnitMapProfile()
        {
            CreateMap<CreateUpdateUnitInputDto, Unit>().ReverseMap();
            CreateMap<UnitDetailDto, Unit>().ReverseMap();
            CreateMap<FindUnitDto, Unit>().ReverseMap();
        }
    }
}
