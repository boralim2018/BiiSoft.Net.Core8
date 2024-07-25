using OfficeOpenXml.Table.PivotTable;
using System.Collections.Generic;

namespace BiiSoft.Configuration
{
    public static class AppSettingNames
    {
        public static class UI
        {
            public const string Enable = "UI.Enable";

            public static class Theme 
            {
                public const string Name = "UI.Theme.Name";
                public const string ColorScheme = "UI.Theme.ColorScheme";
            }         
            
            public static class Options
            {                
                public const string FontSize = "UI.Options.FontSize";
                public const string Ripple = "UI.Options.Ripple";
                public const string InputStyle = "UI.Options.InputStyle";
                public const string MenuType = "UI.Options.MenuType";
            }
        }

        public static class HostManagement
        {
            public const string BillingLegalName = "App.HostManagement.BillingLegalName";
            public const string BillingAddress = "App.HostManagement.BillingAddress";
        }

        public static class TenantManagement
        {
            public const string AllowSelfRegistration = "App.TenantManagement.AllowSelfRegistration";
            public const string IsNewRegisteredTenantActiveByDefault = "App.TenantManagement.IsNewRegisteredTenantActiveByDefault";
            public const string UseCaptchaOnRegistration = "App.TenantManagement.UseCaptchaOnRegistration";
            public const string DefaultEdition = "App.TenantManagement.DefaultEdition";
            public const string SubscriptionExpireNotifyDayCount = "App.TenantManagement.SubscriptionExpireNotifyDayCount";
        }

        public static class UserManagement
        {
            public const string AllowSelfRegistration = "App.UserManagement.AllowSelfRegistration";
            public const string IsNewRegisteredUserActiveByDefault = "App.UserManagement.IsNewRegisteredUserActiveByDefault";
            public const string UseCaptchaOnRegistration = "App.UserManagement.UseCaptchaOnRegistration";
            public const string UseCaptchaOnLogin = "App.UserManagement.UseCaptchaOnLogin";
            public const string SmsVerificationEnabled = "App.UserManagement.SmsVerificationEnabled";
            public const string IsEmailConfirmationRequiredForLogin = "App.UserManagement.IsEmailConfirmationRequiredForLogin";

            public static class TwoFactorLogin
            {
                public const string IsGoogleAuthenticatorEnabled = "App.UserManagement.TwoFactorLogin.IsGoogleAuthenticatorEnabled";
                public const string IsEnabled = "App.UserManagement.TwoFactorLogin.IsEnabled";
                public const string IsEmailProviderEnabled = "App.UserManagement.TwoFactorLogin.IsEmailProviderEnabled";
                public const string IsSmsProviderEnabled = "App.UserManagement.TwoFactorLogin.IsSmsProviderEnabled";
                public const string IsRememberBrowserEnabled = "App.UserManagement.TwoFactorLogin.IsRememberBrowserEnabled";
            }

            public static class UserLockOut
            {
                public const string IsEnabled = "App.UserManagement.UserLockOut.IsEnabled";
                public const string MaxFailedAccessAttemptsBeforeLockout = "App.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout";
                public const string DefaultAccountLockoutSeconds = "App.UserManagement.UserLockOut.DefaultAccountLockoutSeconds";
            }

            public static class PasswordComplexity
            {
                public const string RequiredLength = "App.UserManagement.PasswordComplexity.RequiredLength";
                public const string RequireNonAlphanumeric = "App.UserManagement.PasswordComplexity.RequireNonAlphanumeric";
                public const string RequireLowercase = "App.UserManagement.PasswordComplexity.RequireLowercase";
                public const string RequireUppercase = "App.UserManagement.PasswordComplexity.RequireUppercase";
                public const string RequireDigit = "App.UserManagement.PasswordComplexity.RequireDigit";
            }
        }

        public static class Recaptcha
        {
            public const string SiteKey = "Recaptcha.SiteKey";
        }
    }
}
