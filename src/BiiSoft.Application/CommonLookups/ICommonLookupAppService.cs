using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.Configuration.Dto;
using BiiSoft.Configuration.Host.Dto;
using BiiSoft.Dtos;

namespace BiiSoft.CommonLookups
{
    public interface ICommonLookupAppService : IApplicationService
    {   
        Task<ListResultDto<string>> GetTimeZones(PagedFilterInputDto input);
    }
}
