using AutoMapper;
using BiiSoft.Items;

namespace BiiSoft.ItemBrands.Dto
{
    public class ItemBrandMapProfile : Profile
    {
        public ItemBrandMapProfile()
        {
            CreateMap<CreateUpdateItemBrandInputDto, ItemBrand>().ReverseMap();
            CreateMap<ItemBrandDetailDto, ItemBrand>().ReverseMap();
            CreateMap<FindItemBrandDto, ItemBrand>().ReverseMap();
        }
    }
}
