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
using BiiSoft.CityProvinces.Dto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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