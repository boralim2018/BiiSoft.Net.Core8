using AutoMapper;
using BiiSoft.Locations;

namespace BiiSoft.KhanDistricts.Dto
{
    public class KhanDistrictMapProfile : Profile
    {
        public KhanDistrictMapProfile()
        {
            CreateMap<CreateUpdateKhanDistrictInputDto, KhanDistrict>().ReverseMap();
            CreateMap<KhanDistrictDetailDto, KhanDistrict>().ReverseMap();
        }
    }
}
