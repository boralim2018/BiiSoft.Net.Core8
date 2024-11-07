using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ChartOfAccounts.Dto
{
    public class CreateUpdateChartOfAccountInputDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Code { get; set; }
        public AccountType AccountType { get; set; }
        public SubAccountType SubAccountType { get; set; }
        public Guid? ParentId { get; set; }
    }

}
