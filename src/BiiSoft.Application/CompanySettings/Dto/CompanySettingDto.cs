using BiiSoft.Branches.Dto;
using BiiSoft.MultiTenancy.Dto;
using System.Collections.Generic;

namespace BiiSoft.CompanySettings.Dto
{
    public class CompanySettingDto
    {
        public UpdateLogoInput CompanyLogo { get; set; }
        public BranchDetailDto Branch {  get; set; }
        public CompanyGeneralSettingDto GeneralSetting { get; set; }
        public CompanyAdvanceSettingDto AdvanceSetting { get; set; }
        public List<TransactionNoSettingDto> TransactionNoSettings { get; set; } 

    }
}
