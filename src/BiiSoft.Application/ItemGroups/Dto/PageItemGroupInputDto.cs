using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ItemGroups.Dto
{
    public class PageItemGroupInputDto : PageAuditedAcitveSortFilterInputDto
    {
        
    }

    public class ExportExcelItemGroupInputDto : PageItemGroupInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
