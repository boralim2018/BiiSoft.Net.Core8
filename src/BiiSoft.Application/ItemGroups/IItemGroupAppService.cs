using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.ItemGroups.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ItemGroups
{
    public interface IItemGroupAppService : IApplicationService
    {
        Task<PagedResultDto<ItemGroupListDto>> GetList(PageItemGroupInputDto input);
        Task<Guid> Create(CreateUpdateItemGroupInputDto input);
        Task Update(CreateUpdateItemGroupInputDto input);
        Task<ItemGroupDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task<FindItemGroupDto> GetDefaultValue();
        Task<PagedResultDto<FindItemGroupDto>> Find(PageItemGroupInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelItemGroupInputDto input);
    }
}
