using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ItemGrades.Dto
{
    public class PageItemGradeInputDto : PageAuditedAcitveSortFilterInputDto
    {
        
    }

    public class ExportExcelItemGradeInputDto : PageItemGradeInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
