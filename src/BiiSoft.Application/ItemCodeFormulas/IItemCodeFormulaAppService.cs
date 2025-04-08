using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.ItemCodeFormulas.Dto;

namespace BiiSoft.ItemCodeFormulas
{
    public interface IItemCodeFormulaAppService : IApplicationService
    {
        Task<PagedResultDto<ItemCodeFormulaListDto>> GetList(PageItemCodeFormulaInputDto input);
        Task<Guid> Create(CreateUpdateItemCodeFormulaInputDto input);
        Task Update(CreateUpdateItemCodeFormulaInputDto input);
        Task<ItemCodeFormulaDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);      
    }
}
