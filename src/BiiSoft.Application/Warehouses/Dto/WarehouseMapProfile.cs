using AutoMapper;

namespace BiiSoft.Warehouses.Dto
{
    public class WarehouseMapProfile : Profile
    {
        public WarehouseMapProfile()
        {
            CreateMap<CreateUpdateWarehouseInputDto, Warehouse>().ReverseMap();
            CreateMap<WarehouseDetailDto, Warehouse>().ReverseMap();
            CreateMap<FindWarehouseDto, Warehouse>().ReverseMap();
            CreateMap<WarehouseBranchDto, WarehouseBranch>().ReverseMap();
        }
    }
}
