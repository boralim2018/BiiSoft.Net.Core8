using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.FieldCs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.FieldCs
{
    public interface IFieldCAppService : IApplicationService
    {
        Task<PagedResultDto<FieldCListDto>> GetList(PageFieldCInputDto input);
        Task<Guid> Create(CreateUpdateFieldCInputDto input);
        Task Update(CreateUpdateFieldCInputDto input);
        Task<FieldCDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task<FindFieldCDto> GetDefaultValue();
        Task<PagedResultDto<FindFieldCDto>> Find(PageFieldCInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelFieldCInputDto input);
    }
}
