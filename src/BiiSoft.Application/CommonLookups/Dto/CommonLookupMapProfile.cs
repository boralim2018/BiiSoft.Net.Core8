using AutoMapper;
using BiiSoft.Items;

namespace BiiSoft.CommonLookups.Dto
{
    public class CommonLookupMapProfile : Profile
    {
        public CommonLookupMapProfile()
        {
            CreateMap<ItemFieldSettingDto, ItemFieldSetting>().ReverseMap();
        }
    }
}