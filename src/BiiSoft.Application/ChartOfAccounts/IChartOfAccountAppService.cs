using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.ChartOfAccounts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ChartOfAccounts
{
    public interface IChartOfAccountAppService : IApplicationService
    {
        Task<PagedResultDto<ChartOfAccountListDto>> GetList(PageChartOfAccountInputDto input);
        Task<Guid> Create(CreateUpdateChartOfAccountInputDto input);
        Task Update(CreateUpdateChartOfAccountInputDto input);
        Task<ChartOfAccountDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task<PagedResultDto<FindChartOfAccountDto>> Find(PageChartOfAccountInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelChartOfAccountInputDto input);
    }
}
