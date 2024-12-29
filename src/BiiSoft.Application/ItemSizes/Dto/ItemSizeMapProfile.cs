using AutoMapper;
using BiiSoft.Items;

namespace BiiSoft.ItemSizes.Dto
{
    public class ItemSizeMapProfile : Profile
    {
        public ItemSizeMapProfile()
        {
            CreateMap<CreateUpdateItemSizeInputDto, ItemSize>().ReverseMap();
            CreateMap<ItemSizeDetailDto, ItemSize>().ReverseMap();
            CreateMap<FindItemSizeDto, ItemSize>().ReverseMap();
        }
    }
}
