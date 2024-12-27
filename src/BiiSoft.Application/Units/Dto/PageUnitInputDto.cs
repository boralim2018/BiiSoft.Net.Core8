using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Units.Dto
{
    public class PageUnitInputDto : PageAuditedAcitveSortFilterInputDto
    {
        
    }

    public class ExportExcelUnitInputDto : PageUnitInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
