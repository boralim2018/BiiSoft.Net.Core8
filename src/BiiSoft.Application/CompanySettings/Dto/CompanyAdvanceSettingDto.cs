﻿using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.CompanySettings.Dto
{
    public class CompanyAdvanceSettingDto
    {
        public long? Id { get; set; }
        public bool MultiBranchesEnable { get; set; }
        public bool MultiCurrencyEnable { get; set; }
        public bool LineDiscountEnable { get; set; }
        public bool TotalDiscountEnable { get; set; }
        public bool CustomAccountCodeEnable { get; set; }
        public bool ClassEnable { get; set; }
        public bool TaxEnable { get; set; }
        public TaxType TaxType { get; set; }
    }
}
