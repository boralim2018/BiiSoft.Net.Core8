using AutoMapper;

namespace BiiSoft.BOMs.Dto
{
    public class BOMMapProfile : Profile
    {
        public BOMMapProfile()
        {
            CreateMap<CreateUpdateBOMInputDto, BOM>().ReverseMap();
            CreateMap<BOMDetailDto, BOM>().ReverseMap();
            CreateMap<FindBOMDto, BOM>().ReverseMap();
            CreateMap<BOMItemDto, BOMItem>().ReverseMap();
        }
    }
}
