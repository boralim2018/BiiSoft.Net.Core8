using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Abp.Timing.Timezone;
using BiiSoft.Authorization;
using BiiSoft.CommonLookups.Dto;
using BiiSoft.Enums;
using BiiSoft.Extensions;
using BiiSoft.Timing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<ListResultDto<NameValueDto<AccountType>>> GetAccountTypes()
        {
            var items = new List<NameValueDto<AccountType>>();
            await Task.Run(() => {
                items = Enum.GetValues(typeof(AccountType))
                             .Cast<AccountType>()
                             .Select(s => new NameValueDto<AccountType>(s.GetName(), s))
                             .ToList();
            });

            return new ListResultDto<NameValueDto<AccountType>> { Items = items };
        }

        public async Task<ListResultDto<NameValueDto<SubAccountType>>> GetSubAccountTypes(SubAccountTypeFilterInputDto input)
        {
            var items = new List<NameValueDto<SubAccountType>>();
            await Task.Run(() => {
                items = Enum.GetValues(typeof(SubAccountType))
                             .Cast<SubAccountType>()
                             .WhereIf(input.AccountTypeFilter != null && !input.AccountTypeFilter.Ids.IsNullOrEmpty(), s => 
                                (input.AccountTypeFilter.Exclude && !input.AccountTypeFilter.Ids.Contains(s.Parent())) ||
                                (!input.AccountTypeFilter.Exclude && input.AccountTypeFilter.Ids.Contains(s.Parent())))
                             .Select(s => new NameValueDto<SubAccountType>(s.GetName(), s))
                             .ToList();
            });

            return new ListResultDto<NameValueDto<SubAccountType>> { Items = items };
        }

        public async Task<ListResultDto<string>> GetTimeZones()
        {
            var timezones = new List<string>();           
            await Task.Run(() => { 
                timezones = TimezoneHelper.GetWindowsTimeZoneIds().ToList(); 
            });

            return new ListResultDto<string> { Items = timezones };
        }

        public async Task<ListResultDto<NameValueDto<ItemType>>> GetItemTypes()
        {
            var items = new List<NameValueDto<ItemType>>();
            await Task.Run(() => {
                items = Enum.GetValues(typeof(ItemType))
                             .Cast<ItemType>()
                             .Select(s => new NameValueDto<ItemType>(s.GetName(), s))
                             .ToList();
            });

            return new ListResultDto<NameValueDto<ItemType>> { Items = items };
        }

        public async Task<ListResultDto<NameValueDto<ItemCategory>>> GetItemCategories()
        {
            var items = new List<NameValueDto<ItemCategory>>();
            await Task.Run(() => {
                items = Enum.GetValues(typeof(ItemCategory))
                             .Cast<ItemCategory>()
                             .Select(s => new NameValueDto<ItemCategory>(s.GetName(), s))
                             .ToList();
            });

            return new ListResultDto<NameValueDto<ItemCategory>> { Items = items };
        }

    }
}