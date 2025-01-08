using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.ItemSizes.Dto;
using System;
using System.Threading.Tasks;

namespace BiiSoft.ItemSizes
{
    public interface IItemSizeAppService : IApplicationService
    {
        Task<PagedResultDto<ItemSizeListDto>> GetList(PageItemSizeInputDto input);
        Task<Guid> Create(CreateUpdateItemSizeInputDto input);
        Task Update(CreateUpdateItemSizeInputDto input);
        Task<ItemSizeDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task UnsetAsDefault(EntityDto<Guid> input);
        Task<FindItemSizeDto> GetDefaultValue();
        Task<PagedResultDto<FindItemSizeDto>> Find(PageItemSizeInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelItemSizeInputDto input);
    }
}
