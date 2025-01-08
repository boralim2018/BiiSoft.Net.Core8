using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.ItemBrands.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ItemBrands
{
    public interface IItemBrandAppService : IApplicationService
    {
        Task<PagedResultDto<ItemBrandListDto>> GetList(PageItemBrandInputDto input);
        Task<Guid> Create(CreateUpdateItemBrandInputDto input);
        Task Update(CreateUpdateItemBrandInputDto input);
        Task<ItemBrandDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task UnsetAsDefault(EntityDto<Guid> input);
        Task<FindItemBrandDto> GetDefaultValue();
        Task<PagedResultDto<FindItemBrandDto>> Find(PageItemBrandInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelItemBrandInputDto input);
    }
}
