using BiiSoft.Auditing.Dto;
using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ChartOfAccounts.Dto
{
    public class PageChartOfAccountInputDto : PageAuditedAcitveSortFilterInputDto
    {
        public FilterInputDto<int> AccountTypes { get; set; }
        public FilterInputDto<int> SubAccountTypes { get; set; }
        public FilterInputDto<Guid?> Parents { get; set; }
    }

    public class ExportExcelChartOfAccountInputDto : PageChartOfAccountInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
