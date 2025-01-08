using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.Units.Dto;
using System;
using System.Threading.Tasks;

namespace BiiSoft.Units
{
    public interface IUnitAppService : IApplicationService
    {
        Task<PagedResultDto<UnitListDto>> GetList(PageUnitInputDto input);
        Task<Guid> Create(CreateUpdateUnitInputDto input);
        Task Update(CreateUpdateUnitInputDto input);
        Task<UnitDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task UnsetAsDefault(EntityDto<Guid> input);
        Task<FindUnitDto> GetDefaultValue();
        Task<PagedResultDto<FindUnitDto>> Find(PageUnitInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelUnitInputDto input);
    }
}
