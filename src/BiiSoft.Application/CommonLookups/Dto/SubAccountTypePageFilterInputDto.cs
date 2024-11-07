using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.CommonLookups.Dto
{
    public class SubAccountTypePageFilterInputDto : PagedFilterInputDto
    {
        public List<AccountType> AccountTypes { get; set; }
        public List<SubAccountType> SelectedSubAccountTypes { get; set; } 
    }
}
