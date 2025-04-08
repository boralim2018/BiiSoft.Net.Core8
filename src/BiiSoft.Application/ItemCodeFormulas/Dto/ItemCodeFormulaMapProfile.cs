using AutoMapper;
using BiiSoft.Items;

namespace BiiSoft.ItemCodeFormulas.Dto
{
    public class ItemCodeFormulaMapProfile : Profile
    {
        public ItemCodeFormulaMapProfile()
        {
            CreateMap<CreateUpdateItemCodeFormulaInputDto, ItemCodeFormula>().ReverseMap();
            CreateMap<ItemCodeFormulaDetailDto, ItemCodeFormula>().ReverseMap();
            CreateMap<ItemCodeFormulaItemTypeDto, ItemCodeFormulaItemType>().ReverseMap();
        }
    }
}
