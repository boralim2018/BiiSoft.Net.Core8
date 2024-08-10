using BiiSoft.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.CommonLookups.Dto
{
    public class TimeZonePageFilterInputDto : PagedFilterInputDto
    {
        public List<string> SelectedTimeZones { get; set; } 
    }
}
