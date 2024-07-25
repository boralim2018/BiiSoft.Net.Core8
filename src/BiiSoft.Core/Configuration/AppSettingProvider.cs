using System.Collections.Generic;
using System.Linq;
using Abp.Configuration;
using Microsoft.Extensions.Configuration;

namespace BiiSoft.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        private readonly IConfigurationRoot _appConfiguration;

        public AppSettingProvider(IAppConfigurationAccessor configurationAccessor)
        {
            _appConfiguration = configurationAccessor.Configuration;
        }

        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.UI.Enable, BiiSoftConsts.UISettingEnable.ToString(), scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UI.Theme.Name, BiiSoftConsts.ThemeName, scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UI.Theme.ColorScheme, BiiSoftConsts.ThemeColor, scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),

                new SettingDefinition(AppSettingNames.UI.Options.FontSize, BiiSoftConsts.FontSize.ToString(), scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UI.Options.Ripple, BiiSoftConsts.Ripple.ToString(), scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UI.Options.InputStyle, BiiSoftConsts.InputStyle.ToString(), scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UI.Options.MenuType, BiiSoftConsts.MenuType.ToString(), scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),

                new SettingDefinition(AppSettingNames.UserManagement.AllowSelfRegistration, "false", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin, "false", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UserManagement.SmsVerificationEnabled, "false", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UserManagement.UseCaptchaOnRegistration, "false", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UserManagement.UseCaptchaOnLogin, GetFromAppSettings(AppSettingNames.UserManagement.UseCaptchaOnLogin, "false"), scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UserManagement.IsNewRegisteredUserActiveByDefault, "false", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),

                new SettingDefinition(AppSettingNames.UserManagement.PasswordComplexity.RequireNonAlphanumeric, "false", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UserManagement.PasswordComplexity.RequireLowercase, "false", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UserManagement.PasswordComplexity.RequireUppercase, "false", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UserManagement.PasswordComplexity.RequireDigit, "false", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UserManagement.PasswordComplexity.RequiredLength, "0", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),

                new SettingDefinition(AppSettingNames.UserManagement.TwoFactorLogin.IsEnabled, "false", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UserManagement.TwoFactorLogin.IsGoogleAuthenticatorEnabled, "false", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UserManagement.TwoFactorLogin.IsRememberBrowserEnabled, "false", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UserManagement.TwoFactorLogin.IsSmsProviderEnabled, "false", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UserManagement.TwoFactorLogin.IsEmailProviderEnabled, "false", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                
                new SettingDefinition(AppSettingNames.UserManagement.UserLockOut.IsEnabled, "false", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout, "0", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.UserManagement.UserLockOut.DefaultAccountLockoutSeconds, "0", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                
                new SettingDefinition(AppSettingNames.TenantManagement.DefaultEdition, "", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.TenantManagement.UseCaptchaOnRegistration, "false", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.TenantManagement.AllowSelfRegistration, "false", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.TenantManagement.IsNewRegisteredTenantActiveByDefault, "false", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.TenantManagement.SubscriptionExpireNotifyDayCount, "0", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
               
                new SettingDefinition(AppSettingNames.HostManagement.BillingLegalName, "", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.HostManagement.BillingAddress, "", scopes: SettingScopes.All, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.Recaptcha.SiteKey, GetFromSettings("Recaptcha:SiteKey"), isVisibleToClients: true),
            };
        }


        private string GetFromAppSettings(string name, string defaultValue = null)
        {
            return GetFromSettings("App:" + name, defaultValue);
        }

        private string GetFromSettings(string name, string defaultValue = null)
        {
            return _appConfiguration[name] ?? defaultValue;
        }

    }
}
