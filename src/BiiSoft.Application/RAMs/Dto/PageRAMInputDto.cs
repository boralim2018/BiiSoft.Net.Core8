using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.RAMs.Dto
{
    public class PageRAMInputDto : PageAuditedAcitveSortFilterInputDto
    {
        
    }

    public class ExportExcelRAMInputDto : PageRAMInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
