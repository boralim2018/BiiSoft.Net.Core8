using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.FieldAs.Dto
{
    public class PageFieldAInputDto : PageAuditedAcitveSortFilterInputDto
    {
        
    }

    public class ExportExcelFieldAInputDto : PageFieldAInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
