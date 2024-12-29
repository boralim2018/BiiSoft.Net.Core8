using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ItemModels.Dto
{
    public class PageItemModelInputDto : PageAuditedAcitveSortFilterInputDto
    {
        
    }

    public class ExportExcelItemModelInputDto : PageItemModelInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
