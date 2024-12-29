using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ItemBrands.Dto
{
    public class PageItemBrandInputDto : PageAuditedAcitveSortFilterInputDto
    {
        
    }

    public class ExportExcelItemBrandInputDto : PageItemBrandInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
