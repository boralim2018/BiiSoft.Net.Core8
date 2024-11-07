using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.ChartOfAccounts.Dto
{
    public class ChartOfAccountDetailDto : CanModifyNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
        public string Code { get; set; }
        public AccountType AccountType { get; set; }
        public SubAccountType SubAccountType { get; set; }
        public Guid? ParentId { get; set; }
        public string ParentAccountName { get; set; }

    }
}
