using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.Warehouses.Dto;
using System;
using System.Threading.Tasks;

namespace BiiSoft.Warehouses
{
    public interface IWarehouseAppService : IApplicationService
    {
        Task<PagedResultDto<WarehouseListDto>> GetList(PageWarehouseInputDto input);
        Task<Guid> Create(CreateUpdateWarehouseInputDto input);
        Task Update(CreateUpdateWarehouseInputDto input);
        Task<WarehouseDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task UnsetAsDefault(EntityDto<Guid> input);
        Task<FindWarehouseDto> GetDefaultValue();
        Task<PagedResultDto<FindWarehouseDto>> Find(FindWarehouseInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelWarehouseInputDto input);
    }
}
