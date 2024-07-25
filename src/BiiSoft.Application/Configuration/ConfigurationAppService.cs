using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using BiiSoft.Configuration.Dto;

namespace BiiSoft.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : BiiSoftAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UI.Theme.Name, input.Theme);
        }
    }
}
