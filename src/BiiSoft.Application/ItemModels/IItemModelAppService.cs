using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.ItemModels.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ItemModels
{
    public interface IItemModelAppService : IApplicationService
    {
        Task<PagedResultDto<ItemModelListDto>> GetList(PageItemModelInputDto input);
        Task<Guid> Create(CreateUpdateItemModelInputDto input);
        Task Update(CreateUpdateItemModelInputDto input);
        Task<ItemModelDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task<FindItemModelDto> GetDefaultValue();
        Task<PagedResultDto<FindItemModelDto>> Find(PageItemModelInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelItemModelInputDto input);
    }
}
