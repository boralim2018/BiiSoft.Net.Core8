using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.CommonLookups.Dto;
using BiiSoft.Enums;

namespace BiiSoft.CommonLookups
{
    public interface ICommonLookupAppService : IApplicationService
    {   
        Task<PagedResultDto<string>> GetTimeZones(TimeZonePageFilterInputDto input);
        Task<ListResultDto<NameValueDto<AccountType>>> GetAccountTypes();
        Task<ListResultDto<NameValueDto<SubAccountType>>> GetSubAccountTypes(SubAccountTypeFilterInputDto input);
        Task<ItemFieldSettingDto> GetItemFieldSetting();
    }
}
