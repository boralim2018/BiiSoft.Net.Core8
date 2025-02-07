using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.Items.Dto;
using System;
using System.Threading.Tasks;

namespace BiiSoft.Items
{
    public interface IItemAppService : IApplicationService
    {
        Task<PagedResultDto<ItemListDto>> GetList(PageItemInputDto input);
        Task<Guid> Create(CreateUpdateItemInputDto input);
        Task Update(CreateUpdateItemInputDto input);
        Task<ItemDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task<PagedResultDto<FindItemDto>> Find(PageItemInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelItemInputDto input);
    }
}
