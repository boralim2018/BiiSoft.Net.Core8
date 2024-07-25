using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.BFiles;
using BiiSoft.Currencies.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Currencies
{
    public interface ICurrencyAppService : IApplicationService
    {
        Task<PagedResultDto<CurrencyListDto>> GetList(PageCurrencyInputDto input);
        Task<long> Create(CreateUpdateCurrencyInputDto input);
        Task Update(CreateUpdateCurrencyInputDto input);
        Task<CurrencyDetailDto> GetDetail(EntityDto<long> input);
        Task Delete(EntityDto<long> input);
        Task Enable(EntityDto<long> input);
        Task Disable(EntityDto<long> input);
        Task SetAsDefault(EntityDto<long> input);
        Task<PagedResultDto<FindCurrencyDto>> Find(PageCurrencyInputDto input);
        Task ImportExcel(FileTokenInput input);
        Task<ExportFileOutput> ExportExcelTemplate();
        Task<ExportFileOutput> ExportExcel(ExportExcelCurrencyInputDto input);
    }
}
