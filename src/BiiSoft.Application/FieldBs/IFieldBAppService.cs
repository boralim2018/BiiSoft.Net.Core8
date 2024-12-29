using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.FieldBs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.FieldBs
{
    public interface IFieldBAppService : IApplicationService
    {
        Task<PagedResultDto<FieldBListDto>> GetList(PageFieldBInputDto input);
        Task<Guid> Create(CreateUpdateFieldBInputDto input);
        Task Update(CreateUpdateFieldBInputDto input);
        Task<FieldBDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task<FindFieldBDto> GetDefaultValue();
        Task<PagedResultDto<FindFieldBDto>> Find(PageFieldBInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelFieldBInputDto input);
    }
}
