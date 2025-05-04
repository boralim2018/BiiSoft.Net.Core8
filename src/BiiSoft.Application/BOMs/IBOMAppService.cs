using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.BOMs.Dto;
using System;
using System.Threading.Tasks;

namespace BiiSoft.BOMs
{
    public interface IBOMAppService : IApplicationService
    {
        Task<PagedResultDto<BOMListDto>> GetList(PageBOMInputDto input);
        Task<Guid> Create(CreateUpdateBOMInputDto input);
        Task Update(CreateUpdateBOMInputDto input);
        Task<BOMDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task UnsetAsDefault(EntityDto<Guid> input);
        Task<FindBOMDto> GetDefaultValue();
        Task<PagedResultDto<FindBOMDto>> Find(FindBOMInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelBOMInputDto input);
    }
}
