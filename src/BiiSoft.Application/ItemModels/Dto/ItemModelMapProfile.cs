using AutoMapper;
using BiiSoft.Items;

namespace BiiSoft.ItemModels.Dto
{
    public class ItemModelMapProfile : Profile
    {
        public ItemModelMapProfile()
        {
            CreateMap<CreateUpdateItemModelInputDto, ItemModel>().ReverseMap();
            CreateMap<ItemModelDetailDto, ItemModel>().ReverseMap();
            CreateMap<FindItemModelDto, ItemModel>().ReverseMap();
        }
    }
}
