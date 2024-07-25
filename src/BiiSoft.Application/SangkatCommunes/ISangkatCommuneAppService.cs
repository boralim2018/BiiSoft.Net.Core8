using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.SangkatCommunes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.SangkatCommunes
{
    public interface ISangkatCommuneAppService : IApplicationService
    {
        Task<PagedResultDto<SangkatCommuneListDto>> GetList(PageSangkatCommuneInputDto input);
        Task<Guid> Create(CreateUpdateSangkatCommuneInputDto input);
        Task Update(CreateUpdateSangkatCommuneInputDto input);
        Task<SangkatCommuneDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task<PagedResultDto<FindSangkatCommuneDto>> Find(PageSangkatCommuneInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelSangkatCommuneInputDto input);
    }
}
