using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.Zones.Dto;
using System;
using System.Threading.Tasks;

namespace BiiSoft.Zones
{
    public interface IZoneAppService : IApplicationService
    {
        Task<PagedResultDto<ZoneListDto>> GetList(PageZoneInputDto input);
        Task<Guid> Create(CreateUpdateZoneInputDto input);
        Task Update(CreateUpdateZoneInputDto input);
        Task<ZoneDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task<FindZoneDto> GetDefaultValue();
        Task<PagedResultDto<FindZoneDto>> Find(FindZoneInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelZoneInputDto input);
    }
}
