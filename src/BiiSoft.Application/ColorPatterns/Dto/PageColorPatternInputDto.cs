using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ColorPatterns.Dto
{
    public class PageColorPatternInputDto : PageAuditedAcitveSortFilterInputDto
    {
        
    }

    public class ExportExcelColorPatternInputDto : PageColorPatternInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
