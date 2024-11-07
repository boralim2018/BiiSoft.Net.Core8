using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.CompanySettings.Dto
{
    public class CreateUpdateCompanyAdvanceSettingInputDto
    {
        public long? Id { get; set; }
        public bool MultiBranchesEnable { get; set; }
        public bool MultiCurrencyEnable { get; set; }
        public bool LineDiscountEnable { get; set; }
        public bool TotalDiscountEnable { get; set; }
        public bool CustomAccountCodeEnable { get; set; }
        public bool ClassEnable { get; set; }
      
    }
}
