using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.Cameras.Dto;
using System;
using System.Threading.Tasks;

namespace BiiSoft.Cameras
{
    public interface ICameraAppService : IApplicationService
    {
        Task<PagedResultDto<CameraListDto>> GetList(PageCameraInputDto input);
        Task<Guid> Create(CreateUpdateCameraInputDto input);
        Task Update(CreateUpdateCameraInputDto input);
        Task<CameraDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task UnsetAsDefault(EntityDto<Guid> input);
        Task<FindCameraDto> GetDefaultValue();
        Task<PagedResultDto<FindCameraDto>> Find(PageCameraInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelCameraInputDto input);
    }
}
