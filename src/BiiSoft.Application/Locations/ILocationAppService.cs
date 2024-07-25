using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.Locations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Locations
{
    public interface ILocationAppService : IApplicationService
    {
        Task<PagedResultDto<LocationListDto>> GetList(PageLocationInputDto input);
        Task<Guid> Create(CreateUpdateLocationInputDto input);
        Task Update(CreateUpdateLocationInputDto input);
        Task<LocationDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task<PagedResultDto<FindLocationDto>> Find(PageLocationInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelLocationInputDto input);
    }
}
