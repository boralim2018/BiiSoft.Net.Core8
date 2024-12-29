using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ItemSizes.Dto
{
    public class PageItemSizeInputDto : PageAuditedAcitveSortFilterInputDto
    {
        
    }

    public class ExportExcelItemSizeInputDto : PageItemSizeInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
