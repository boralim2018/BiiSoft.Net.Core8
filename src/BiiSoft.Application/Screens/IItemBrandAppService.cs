using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.Screens.Dto;
using System;
using System.Threading.Tasks;

namespace BiiSoft.Screens
{
    public interface IScreenAppService : IApplicationService
    {
        Task<PagedResultDto<ScreenListDto>> GetList(PageScreenInputDto input);
        Task<Guid> Create(CreateUpdateScreenInputDto input);
        Task Update(CreateUpdateScreenInputDto input);
        Task<ScreenDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task UnsetAsDefault(EntityDto<Guid> input);
        Task<FindScreenDto> GetDefaultValue();
        Task<PagedResultDto<FindScreenDto>> Find(PageScreenInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelScreenInputDto input);
    }
}
