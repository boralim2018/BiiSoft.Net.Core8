using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.HDDs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.HDDs
{
    public interface IHDDAppService : IApplicationService
    {
        Task<PagedResultDto<HDDListDto>> GetList(PageHDDInputDto input);
        Task<Guid> Create(CreateUpdateHDDInputDto input);
        Task Update(CreateUpdateHDDInputDto input);
        Task<HDDDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task<FindHDDDto> GetDefaultValue();
        Task<PagedResultDto<FindHDDDto>> Find(PageHDDInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelHDDInputDto input);
    }
}
