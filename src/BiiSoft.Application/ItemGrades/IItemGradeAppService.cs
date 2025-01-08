using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.ItemGrades.Dto;
using System;
using System.Threading.Tasks;

namespace BiiSoft.ItemGrades
{
    public interface IItemGradeAppService : IApplicationService
    {
        Task<PagedResultDto<ItemGradeListDto>> GetList(PageItemGradeInputDto input);
        Task<Guid> Create(CreateUpdateItemGradeInputDto input);
        Task Update(CreateUpdateItemGradeInputDto input);
        Task<ItemGradeDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task UnsetAsDefault(EntityDto<Guid> input);
        Task<FindItemGradeDto> GetDefaultValue();
        Task<PagedResultDto<FindItemGradeDto>> Find(PageItemGradeInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelItemGradeInputDto input);
    }
}
