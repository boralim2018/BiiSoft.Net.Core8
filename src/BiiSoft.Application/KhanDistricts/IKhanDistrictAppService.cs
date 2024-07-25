using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.KhanDistricts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.KhanDistricts
{
    public interface IKhanDistrictAppService : IApplicationService
    {
        Task<PagedResultDto<KhanDistrictListDto>> GetList(PageKhanDistrictInputDto input);
        Task<Guid> Create(CreateUpdateKhanDistrictInputDto input);
        Task Update(CreateUpdateKhanDistrictInputDto input);
        Task<KhanDistrictDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task<PagedResultDto<FindKhanDistrictDto>> Find(PageKhanDistrictInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelKhanDistrictInputDto input);
    }
}
