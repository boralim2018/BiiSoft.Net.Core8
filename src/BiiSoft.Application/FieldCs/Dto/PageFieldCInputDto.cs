using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.FieldCs.Dto
{
    public class PageFieldCInputDto : PageAuditedAcitveSortFilterInputDto
    {
        
    }

    public class ExportExcelFieldCInputDto : PageFieldCInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
