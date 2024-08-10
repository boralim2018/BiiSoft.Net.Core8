using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.CommonLookups.Dto;

namespace BiiSoft.CommonLookups
{
    public interface ICommonLookupAppService : IApplicationService
    {   
        Task<ListResultDto<string>> GetTimeZones(TimeZonePageFilterInputDto input);
    }
}
