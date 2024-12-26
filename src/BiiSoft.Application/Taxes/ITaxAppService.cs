using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.Taxes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Taxes
{
    public interface ITaxAppService : IApplicationService
    {
        Task<PagedResultDto<TaxListDto>> GetList(PageTaxInputDto input);
        Task<Guid> Create(CreateUpdateTaxInputDto input);
        Task Update(CreateUpdateTaxInputDto input);
        Task<TaxDetailDto> GetDetail(EntityDto<Guid> input);
        Task Delete(EntityDto<Guid> input);
        Task Enable(EntityDto<Guid> input);
        Task Disable(EntityDto<Guid> input);
        Task SetAsDefault(EntityDto<Guid> input);
        Task<PagedResultDto<FindTaxDto>> Find(PageTaxInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelTaxInputDto input);
    }
}
