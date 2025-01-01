namespace BiiSoft.Sessions.Dto
{
    public class GetCurrentLoginInformationsOutput
    {
        public ApplicationInfoDto Application { get; set; }
        public UserLoginInfoDto User { get; set; }
        public TenantLoginInfoDto Tenant { get; set; }
        public GeneralSettingDto GeneralSetting { get; set; }
        public AdvanceSettingDto AdvanceSetting { get; set; }
        public ItemFieldSettingDto ItemFieldSetting { get; set; }
    }

}
