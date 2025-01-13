using AutoMapper;

namespace BiiSoft.Branches.Dto
{
    public class BranchMapProfile : Profile
    {
        public BranchMapProfile()
        {
            CreateMap<CreateUpdateBranchInputDto, Branch>().ReverseMap();
            CreateMap<BranchDetailDto, Branch>().ReverseMap();
            CreateMap<BranchUserDto, BranchUser>().ReverseMap();
        }
    }
}
