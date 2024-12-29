using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.CPUs.Dto
{
    public class PageCPUInputDto : PageAuditedAcitveSortFilterInputDto
    {
        
    }

    public class ExportExcelCPUInputDto : PageCPUInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
