using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.FieldAs.Dto;
using System;
using System.Threading.Tasks;

namespace BiiSoft.FieldAs
{
    public interface IFieldAAppService : IApplicationService
    {
        Task<PagedResultDto<FieldAListDto>> GetList(PageFieldAInputDto input);
        Task<Guid> Create(CreateUpdateFieldAInputDto input);
        Task Update(CreateUpdateFieldAInputDto input);
        Task<FieldADetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task UnsetAsDefault(EntityDto<Guid> input);
        Task<FindFieldADto> GetDefaultValue();
        Task<PagedResultDto<FindFieldADto>> Find(PageFieldAInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelFieldAInputDto input);
    }
}
