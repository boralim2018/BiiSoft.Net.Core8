using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Extensions;
using Abp.Timing.Timezone;
using BiiSoft.Authorization;
using BiiSoft.Timing;
using BiiSoft.CommonLookups.Dto;
using BiiSoft.Enums;
using BiiSoft.Extensions;
using Abp.Collections.Extensions;

namespace BiiSoft.CommonLookups
{
    [AbpAuthorize(PermissionNames.Pages_Find)]
    public class CommonLookupAppService : BiiSoftAppServiceBase, ICommonLookupAppService
    {
        private readonly ITimeZoneService _timeZoneService;

        public CommonLookupAppService(
            ITimeZoneService timeZoneService) : base()
        {
            _timeZoneService = timeZoneService;
        }

        public async Task<PagedResultDto<NameValueDto<AccountType>>> GetAccountTypes(AccountTypePageFilterInputDto input)
        {
            var models = new List<AccountType>();
            await Task.Run(() => {
                models = Enum.GetValues(typeof(AccountType))
                             .Cast<AccountType>()
                             .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s => s.GetName().ToLower().Contains(input.Keyword.ToLower()))
                             .OrderBy(x => input.SelectedAccountTypes.IsNullOrEmpty() || input.SelectedAccountTypes.Contains(x) ? 0 : 1)
                             .ToList();
            });

            var totalCount = models.Count;
            var items = new List<NameValueDto<AccountType>>();
            if (totalCount > 0)
            {
                if (input.UsePagination)
                {
                    items = models.Skip(input.SkipCount).Take(input.MaxResultCount).Select(s => new NameValueDto<AccountType>(s.GetName(), s)).ToList();
                }
                else
                {
                    items = models.Select(s => new NameValueDto<AccountType>(s.GetName(), s)).ToList();
                }
            }

            return new PagedResultDto<NameValueDto<AccountType>> { Items = items, TotalCount = totalCount };
        }

        public async Task<PagedResultDto<NameValueDto<SubAccountType>>> GetSubAccountTypes(SubAccountTypePageFilterInputDto input)
        {
            var models = new List<SubAccountType>();
            await Task.Run(() => {
                models = Enum.GetValues(typeof(SubAccountType))
                             .Cast<SubAccountType>()
                             .WhereIf(!input.AccountTypes.IsNullOrEmpty(), s => input.AccountTypes.Contains(s.Parent()))
                             .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s => s.GetName().ToLower().Contains(input.Keyword.ToLower()))
                             .OrderBy(x => input.SelectedSubAccountTypes.IsNullOrEmpty() || input.SelectedSubAccountTypes.Contains(x) ? 0 : 1)
                             .ToList();
            });

            var totalCount = models.Count;
            var items = new List<NameValueDto<SubAccountType>>();
            if (totalCount > 0)
            {
                if (input.UsePagination)
                {
                    items = models.Skip(input.SkipCount).Take(input.MaxResultCount).Select(s => new NameValueDto<SubAccountType>(s.GetName(), s)).ToList();
                }
                else
                {
                    items = models.Select(s => new NameValueDto<SubAccountType>(s.GetName(), s)).ToList();
                }
            }

            return new PagedResultDto<NameValueDto<SubAccountType>> { Items = items, TotalCount = totalCount };
        }

        public async Task<PagedResultDto<string>> GetTimeZones(TimeZonePageFilterInputDto input)
        {
            var timezones = new List<string>();           
            await Task.Run(() => { 
                timezones = TimezoneHelper.GetWindowsTimeZoneIds()
                            .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s => s.ToLower().Contains(input.Keyword.ToLower()))
                            .OrderBy(x => input.SelectedTimeZones.IsNullOrEmpty() || input.SelectedTimeZones.Contains(x) ? 0 : 1 )
                            .ToList(); 
            });

            var totalCount = timezones.Count;
            var items = new List<string>();
            if (totalCount > 0)
            {
                if (input.UsePagination)
                {
                    items = timezones.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
                }
                else
                {
                    items = timezones;
                }
            }

            return new PagedResultDto<string> { Items = items, TotalCount = totalCount };
        }

    }
}