using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.Branches.Dto;
using System;
using System.Threading.Tasks;

namespace BiiSoft.Branches
{
    public interface IBranchAppService : IApplicationService
    {
        Task<PagedResultDto<BranchListDto>> GetList(PageBranchInputDto input);
        Task<Guid> Create(CreateUpdateBranchInputDto input);
        Task Update(CreateUpdateBranchInputDto input);
        Task<BranchDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task UnsetAsDefault(EntityDto<Guid> input);
        Task<FindBranchDto> GetDefaultValue();
        Task<PagedResultDto<FindBranchDto>> Find(PageBranchInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelBranchInputDto input);
    }
}
