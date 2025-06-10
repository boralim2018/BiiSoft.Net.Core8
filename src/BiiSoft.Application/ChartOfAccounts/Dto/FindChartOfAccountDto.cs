using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.ChartOfAccounts.Dto
{
    public class FindChartOfAccountDto : NameActiveDto<Guid>
    {      
        public string Code { get; set; }
        public AccountType AccountType { get; set; }
        public SubAccountType SubAccountType { get; set; }
        public string AccountTypeName { get; set; }
        public string SubAccountTypeName { get; set; }
        public string ParentAccount {  get; set; }
    }
}
