using AutoMapper;
using BiiSoft.Items;

namespace BiiSoft.Batteries.Dto
{
    public class BatteryMapProfile : Profile
    {
        public BatteryMapProfile()
        {
            CreateMap<CreateUpdateBatteryInputDto, Battery>().ReverseMap();
            CreateMap<BatteryDetailDto, Battery>().ReverseMap();
            CreateMap<FindBatteryDto, Battery>().ReverseMap();
        }
    }
}
