using BiiSoft.Auditing.Dto;
using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Currencies.Dto
{
    public class PageCurrencyInputDto : PageAuditedAcitveSortFilterInputDto
    {
        
    }

    public class ExportExcelCurrencyInputDto : PageCurrencyInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
