using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.ChartOfAccounts.Dto
{
    public class FindChartOfAccountDto : NameActiveDto<Guid>
    {      
        public string Code { get; set; }
        public string AccountType { get; set; }
        public string SubAccountType { get; set; }
        public string ParentAccount {  get; set; }
    }
}
