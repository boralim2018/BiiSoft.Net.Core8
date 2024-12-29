using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.ColorPatterns.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ColorPatterns
{
    public interface IColorPatternAppService : IApplicationService
    {
        Task<PagedResultDto<ColorPatternListDto>> GetList(PageColorPatternInputDto input);
        Task<Guid> Create(CreateUpdateColorPatternInputDto input);
        Task Update(CreateUpdateColorPatternInputDto input);
        Task<ColorPatternDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task<FindColorPatternDto> GetDefaultValue();
        Task<PagedResultDto<FindColorPatternDto>> Find(PageColorPatternInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelColorPatternInputDto input);
    }
}
