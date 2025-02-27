using AutoMapper;

namespace BiiSoft.Items.Dto
{
    public class ItemMapProfile : Profile
    {
        public ItemMapProfile()
        {
            CreateMap<CreateUpdateItemInputDto, Item>().ReverseMap();
            CreateMap<ItemDetailDto, Item>().ReverseMap();
            CreateMap<ItemZoneDto, ItemZone>().ReverseMap();
            CreateMap<ItemSettingDto, ItemSetting>().ReverseMap();
            CreateMap<ItemFieldSettingDto, ItemFieldSetting>().ReverseMap();
        }
    }
}
