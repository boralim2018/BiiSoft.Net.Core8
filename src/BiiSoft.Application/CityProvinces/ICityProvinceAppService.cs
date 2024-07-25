using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.CityProvinces.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.CityProvinces
{
    public interface ICityProvinceAppService : IApplicationService
    {
        Task<PagedResultDto<CityProvinceListDto>> GetList(PageCityProvinceInputDto input);
        Task<Guid> Create(CreateUpdateCityProvinceInputDto input);
        Task Update(CreateUpdateCityProvinceInputDto input);
        Task<CityProvinceDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task<PagedResultDto<FindCityProvinceDto>> Find(PageCityProvinceInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelCityProvinceInputDto input);
    }
}
