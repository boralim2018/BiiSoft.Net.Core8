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
        public FilterInputDto<AccountType> AccountTypeFilter { get; set; }
        public FilterInputDto<SubAccountType> SubAccountTypeFilter { get; set; }
        public FilterInputDto<Guid?> ParentFilter { get; set; }

    }

    public class FindChartOfAccountInputDto : PageChartOfAccountInputDto
    {
        public bool ExcludeSubAccount { get; set; }
    }

    public class ExportExcelChartOfAccountInputDto : PageChartOfAccountInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
