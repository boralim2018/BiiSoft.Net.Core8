using AutoMapper;
using BiiSoft.Items;

namespace BiiSoft.ItemGrades.Dto
{
    public class ItemGradeMapProfile : Profile
    {
        public ItemGradeMapProfile()
        {
            CreateMap<CreateUpdateItemGradeInputDto, ItemGrade>().ReverseMap();
            CreateMap<ItemGradeDetailDto, ItemGrade>().ReverseMap();
            CreateMap<FindItemGradeDto, ItemGrade>().ReverseMap();
        }
    }
}
