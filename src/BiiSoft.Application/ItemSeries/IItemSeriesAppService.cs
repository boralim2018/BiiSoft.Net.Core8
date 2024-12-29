using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.Items.Series.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Items.Series
{
    public interface IItemSeriesAppService : IApplicationService
    {
        Task<PagedResultDto<ItemSeriesListDto>> GetList(PageItemSeriesInputDto input);
        Task<Guid> Create(CreateUpdateItemSeriesInputDto input);
        Task Update(CreateUpdateItemSeriesInputDto input);
        Task<ItemSeriesDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task<FindItemSeriesDto> GetDefaultValue();
        Task<PagedResultDto<FindItemSeriesDto>> Find(PageItemSeriesInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelItemSeriesInputDto input);
    }
}
