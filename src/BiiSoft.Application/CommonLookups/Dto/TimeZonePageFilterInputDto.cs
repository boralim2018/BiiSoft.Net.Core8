using BiiSoft.Dtos;
using System.Collections.Generic;

namespace BiiSoft.CommonLookups.Dto
{
    public class TimeZonePageFilterInputDto : PagedFilterInputDto
    {
        public List<string> SelectedTimeZones { get; set; } 
    }
}
