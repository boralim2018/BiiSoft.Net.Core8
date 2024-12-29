using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.FieldBs.Dto
{
    public class PageFieldBInputDto : PageAuditedAcitveSortFilterInputDto
    {
        
    }

    public class ExportExcelFieldBInputDto : PageFieldBInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
