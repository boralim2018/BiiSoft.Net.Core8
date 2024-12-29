using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.RAMs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.RAMs
{
    public interface IRAMAppService : IApplicationService
    {
        Task<PagedResultDto<RAMListDto>> GetList(PageRAMInputDto input);
        Task<Guid> Create(CreateUpdateRAMInputDto input);
        Task Update(CreateUpdateRAMInputDto input);
        Task<RAMDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task<FindRAMDto> GetDefaultValue();
        Task<PagedResultDto<FindRAMDto>> Find(PageRAMInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelRAMInputDto input);
    }
}
