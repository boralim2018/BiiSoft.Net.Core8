using BiiSoft.Branches.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.CompanySettings.Dto
{
    public class CompanySettingDto
    {
        public BranchDetailDto Branch {  get; set; }
        public CompanyGeneralSettingDto GeneralSetting { get; set; }
        public CompanyAdvanceSettingDto AdvanceSetting { get; set; }

    }
}
