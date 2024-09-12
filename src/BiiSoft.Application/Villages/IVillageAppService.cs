using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.Villages.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Villages
{
    public interface IVillageAppService : IApplicationService
    {
        Task<PagedResultDto<VillageListDto>> GetList(PageVillageInputDto input);
        Task<Guid> Create(CreateUpdateVillageInputDto input);
        Task Update(CreateUpdateVillageInputDto input);
        Task<VillageDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task<PagedResultDto<FindVillageDto>> Find(PageVillageInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelVillageInputDto input);
    }
}
