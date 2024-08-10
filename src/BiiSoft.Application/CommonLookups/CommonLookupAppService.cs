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
using Abp.Collections.Extensions;
using BiiSoft.CommonLookups.Dto;

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


        public async Task<ListResultDto<string>> GetTimeZones(TimeZonePageFilterInputDto input)
        {
            var timezones = new List<string>();           
            await Task.Run(() => { 
                timezones = TimezoneHelper.GetWindowsTimeZoneIds()
                            .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s => s.ToLower().Contains(input.Keyword.ToLower()))
                            .OrderBy(x => input.SelectedTimeZones.IsNullOrEmpty() || input.SelectedTimeZones.Contains(x) ? 0 : 1 )
                            .Skip(input.SkipCount)
                            .Take(input.MaxResultCount)
                            .ToList(); 
            });
            return new ListResultDto<string> { Items = timezones };
        }

    }
}