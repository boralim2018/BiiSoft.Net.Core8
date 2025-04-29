using BiiSoft.CompanySettings.Dto;
using BiiSoft.Items.Dto;

namespace BiiSoft.Sessions.Dto
{
    public class GetCurrentLoginInformationsOutput
    {
        public ApplicationInfoDto Application { get; set; }
        public UserLoginInfoDto User { get; set; }
        public TenantLoginInfoDto Tenant { get; set; }
        public CompanyGeneralSettingDto GeneralSetting { get; set; }
        public CompanyAdvanceSettingDto AdvanceSetting { get; set; }
        public ItemSettingDto ItemSetting { get; set; }
    }

}
