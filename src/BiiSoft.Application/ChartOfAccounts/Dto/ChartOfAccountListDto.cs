using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.ChartOfAccounts.Dto
{
    public class ChartOfAccountListDto : CanModifyNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public string Code { get; set; }
        public string AccountType { get; set; }
        public string SubAccountType { get; set; }
        public Guid? ParentId { get; set; }
        public string ParentAccountName { get; set; }
    }
}
