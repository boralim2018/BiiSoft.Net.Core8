using AutoMapper;
using BiiSoft.Items;

namespace BiiSoft.ItemGroups.Dto
{
    public class ItemGroupMapProfile : Profile
    {
        public ItemGroupMapProfile()
        {
            CreateMap<CreateUpdateItemGroupInputDto, ItemGroup>().ReverseMap();
            CreateMap<ItemGroupDetailDto, ItemGroup>().ReverseMap();
            CreateMap<FindItemGroupDto, ItemGroup>().ReverseMap();
        }
    }
}
