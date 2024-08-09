using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Extensions;
using Abp.Net.Mail;
using Abp.Runtime.Security;
using Abp.Timing;
using Abp.Timing.Timezone;
using BiiSoft.Authorization;
using BiiSoft.Configuration.Dto;
using BiiSoft.Configuration.Host.Dto;
using BiiSoft.Editions;
using BiiSoft.Security;
using BiiSoft.Timing;
using BiiSoft.Dtos;
using Abp.Collections.Extensions;
using BiiSoft.CommonLookups;

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


        public async Task<ListResultDto<string>> GetTimeZones(PagedFilterInputDto input)
        {
            var timezones = new List<string>();           
            await Task.Run(() => { 
                timezones = TimezoneHelper.GetWindowsTimeZoneIds()
                            .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s => s.ToLower().Contains(input.Keyword.ToLower()))
                            .Skip(input.SkipCount)
                            .Take(input.MaxResultCount)
                            .ToList(); 
            });
            return new ListResultDto<string> { Items = timezones };
        }

    }
}