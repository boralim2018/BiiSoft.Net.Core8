using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Batteries.Dto
{
    public class PageBatteryInputDto : PageAuditedAcitveSortFilterInputDto
    {
        
    }

    public class ExportExcelBatteryInputDto : PageBatteryInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
