using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.CPUs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.CPUs
{
    public interface ICPUAppService : IApplicationService
    {
        Task<PagedResultDto<CPUListDto>> GetList(PageCPUInputDto input);
        Task<Guid> Create(CreateUpdateCPUInputDto input);
        Task Update(CreateUpdateCPUInputDto input);
        Task<CPUDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task<FindCPUDto> GetDefaultValue();
        Task<PagedResultDto<FindCPUDto>> Find(PageCPUInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelCPUInputDto input);
    }
}
