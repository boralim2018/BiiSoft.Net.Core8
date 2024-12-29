using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.HDDs.Dto
{
    public class PageHDDInputDto : PageAuditedAcitveSortFilterInputDto
    {
        
    }

    public class ExportExcelHDDInputDto : PageHDDInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
