using AutoMapper;
using BiiSoft.Items;

namespace BiiSoft.Cameras.Dto
{
    public class CameraMapProfile : Profile
    {
        public CameraMapProfile()
        {
            CreateMap<CreateUpdateCameraInputDto, Camera>().ReverseMap();
            CreateMap<CameraDetailDto, Camera>().ReverseMap();
            CreateMap<FindCameraDto, Camera>().ReverseMap();
        }
    }
}
