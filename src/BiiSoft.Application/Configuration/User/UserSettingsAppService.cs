using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Net.Mail;
using BiiSoft.Authorization;
using Abp.Runtime.Session;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BiiSoft.Configuration.User
{
    [AbpAuthorize(PermissionNames.Pages_Profile)]
    public class UserSettingsAppService : SettingsAppServiceBase, IUserSettingsAppService
    {
        //private readonly IBiiSoftRepository<Setting, long> _settingRepository;
       
        public UserSettingsAppService(
            ) : base()
        {
            //_settingRepository = settingRepository;
        }

        //public async Task Reset()
        //{
        //    var settingList = new List<string>
        //    {
        //        AppSettingNames.UI.Options.FontSize,
        //        AppSettingNames.UI.Options.InputStyle,
        //        AppSettingNames.UI.Options.MenuType,
        //        AppSettingNames.UI.Options.Ripple,
        //        AppSettingNames.UI.Theme.Name,
        //        AppSettingNames.UI.Theme.ColorScheme,
        //    };
        //    var userSettings = await _settingRepository.GetAll().AsNoTracking()
        //                             .Where(s => s.UserId == AbpSession.UserId)
        //                             .Where(s => settingList.Contains(s.Name)).ToListAsync();
        //    if (userSettings.Any()) await _settingRepository.BulkDeleteAsync(userSettings);
        //}

        //public async Task UpdateFontSize(int input)
        //{
        //    await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UI.Options.FontSize, input.ToString());
        //}

        //public async Task UpdateInputStyle(string input)
        //{
        //    await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UI.Options.InputStyle, input.ToString());
        //}

        //public async Task UpdateMenuType(string input)
        //{
        //    await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UI.Options.MenuType, input.ToString());
        //}

        //public async Task UpdateRipple(bool input)
        //{
        //    await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UI.Options.Ripple, input.ToString());
        //}

        //public async Task UpdateTheme(string name, string dark)
        //{
        //    await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UI.Theme.Name, name);
        //    await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UI.Theme.ColorScheme, dark);
        //}

        public async Task UpdateUIEnable(bool input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UI.Enable, input.ToString());
        }

    }
}