using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.VGAs.Dto
{
    public class PageVGAInputDto : PageAuditedAcitveSortFilterInputDto
    {
        
    }

    public class ExportExcelVGAInputDto : PageVGAInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
