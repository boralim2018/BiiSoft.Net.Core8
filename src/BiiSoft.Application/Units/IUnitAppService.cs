using Abp.Application.Services;
using BiiSoft.Units.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using BiiSoft.Dtos;

namespace BiiSoft.Units
{
    public interface IUnitAppService : IApplicationService
    {
        Task<long> BulkInsert();
        Task<PagedResultDto<UnitDto>> Find(PagedActiveSortFilterInputDto input);
        Task TestPdf();
    }
}
