using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Cameras.Dto
{
    public class PageCameraInputDto : PageAuditedAcitveSortFilterInputDto
    {
        
    }

    public class ExportExcelCameraInputDto : PageCameraInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
