using AutoMapper;
using BiiSoft.Items;

namespace BiiSoft.Sessions.Dto
{
    public class SessionMapProfile : Profile
    {
        public SessionMapProfile()
        {
            CreateMap<ItemFieldSettingDto, ItemFieldSetting>().ReverseMap();
        }
    }
}