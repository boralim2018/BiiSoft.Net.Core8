using BiiSoft.Branches.Dto;
using BiiSoft.MultiTenancy.Dto;

namespace BiiSoft.CompanySettings.Dto
{
    public class CompanySettingDto
    {
        public UpdateLogoInput CompanyLogo { get; set; }
        public BranchDetailDto Branch {  get; set; }
        public CompanyGeneralSettingDto GeneralSetting { get; set; }
        public CompanyAdvanceSettingDto AdvanceSetting { get; set; }

    }
}
