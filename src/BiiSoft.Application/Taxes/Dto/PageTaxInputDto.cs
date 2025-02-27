using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Taxes.Dto
{
    public class PageTaxInputDto : PageAuditedAcitveSortFilterInputDto
    {
        
    }

    public class ExportExcelTaxInputDto : PageTaxInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
