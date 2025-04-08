using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ItemCodeFormulas.Dto
{
    public class PageItemCodeFormulaInputDto : PageAuditedAcitveSortFilterInputDto
    {
        
    }

    public class ExportExcelItemCodeFormulaInputDto : PageItemCodeFormulaInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
