using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.CommonLookups.Dto;
using BiiSoft.Enums;
using System.Threading.Tasks;

namespace BiiSoft.CommonLookups
{
    public interface ICommonLookupAppService : IApplicationService
    {   
        Task<PagedResultDto<string>> GetTimeZones(TimeZonePageFilterInputDto input);
        Task<ListResultDto<NameValueDto<AccountType>>> GetAccountTypes();
        Task<ListResultDto<NameValueDto<SubAccountType>>> GetSubAccountTypes(SubAccountTypeFilterInputDto input);
    }
}
