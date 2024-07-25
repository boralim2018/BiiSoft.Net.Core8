using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Extensions;
using Abp.Net.Mail;
using Abp.Runtime.Security;
using Abp.Timing;
using Abp.Timing.Timezone;
using BiiSoft.Authorization;
using BiiSoft.Configuration.Dto;
using BiiSoft.Configuration.Host.Dto;
using BiiSoft.Editions;
using BiiSoft.Security;
using BiiSoft.Timing;
using BiiSoft.Dtos;
using Abp.Collections.Extensions;

namespace BiiSoft.Configuration.Host
{
    [AbpAuthorize(PermissionNames.Pages_Settings_Host)]
    public class HostSettingsAppService : SettingsAppServiceBase, IHostSettingsAppService
    {
        private readonly EditionManager _editionManager;
        private readonly ITimeZoneService _timeZoneService;
        readonly ISettingDefinitionManager _settingDefinitionManager;

        public HostSettingsAppService(
            EditionManager editionManager,
            ITimeZoneService timeZoneService,
            ISettingDefinitionManager settingDefinitionManager) : base()
        {
            _editionManager = editionManager;
            _timeZoneService = timeZoneService;
            _settingDefinitionManager = settingDefinitionManager;
        }

        #region Get Settings

        public async Task<HostSettingsEditDto> GetAllSettings()
        {

            var general = await GetGeneralSettingsAsync();
            var tenantManagement = await GetTenantManagementSettingsAsync();
            var userManagement = await GetUserManagementAsync();
            var email = await GetEmailSettingsAsync();
            var security = await GetSecuritySettingsAsync();
            var billing = await GetBillingSettingsAsync();

            return new HostSettingsEditDto
            {
                General = general,
                TenantManagement = tenantManagement,
                UserManagement = userManagement,
                Email = email,
                Security = security,
                Billing = billing
            };
        }

        private async Task<GeneralSettingsEditDto> GetGeneralSettingsAsync()
        {
            var timezone = await SettingManager.GetSettingValueForApplicationAsync(TimingSettingNames.TimeZone);
            var settings = new GeneralSettingsEditDto
            {
                Timezone = timezone,
                TimezoneForComparison = timezone
            };

            var defaultTimeZoneId = await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.Application, AbpSession.TenantId);
            if (settings.Timezone == defaultTimeZoneId)
            {
                settings.Timezone = string.Empty;
            }

            return settings;
        }

        private async Task<TenantManagementSettingsEditDto> GetTenantManagementSettingsAsync()
        {
            var settings = new TenantManagementSettingsEditDto
            {
                AllowSelfRegistration = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.TenantManagement.AllowSelfRegistration),
                IsNewRegisteredTenantActiveByDefault = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.TenantManagement.IsNewRegisteredTenantActiveByDefault),
                UseCaptchaOnRegistration = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.TenantManagement.UseCaptchaOnRegistration)
            };

            var defaultEditionId = await SettingManager.GetSettingValueAsync(AppSettingNames.TenantManagement.DefaultEdition);
            if (!defaultEditionId.IsNullOrEmpty())
            {
                var edition = await _editionManager.FindByIdAsync(Convert.ToInt32(defaultEditionId));
                if (!string.IsNullOrEmpty(defaultEditionId) && (edition != null))
                {
                    settings.DefaultEditionId = Convert.ToInt32(defaultEditionId);
                    settings.DefaultEditionName = edition.Name;
                }
            }

            return settings;
        }

        private async Task<HostUserManagementSettingsEditDto> GetUserManagementAsync()
        {
            return new HostUserManagementSettingsEditDto
            {
                //IsEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin),
                //SmsVerificationEnabled = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.SmsVerificationEnabled)

                IsEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin),
                SmsVerificationEnabled = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.SmsVerificationEnabled)
            };
        }

        private async Task<EmailSettingsEditDto> GetEmailSettingsAsync()
        {
            var smtpPassword = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Password);

            return new EmailSettingsEditDto
            {
                DefaultFromAddress = await SettingManager.GetSettingValueAsync(EmailSettingNames.DefaultFromAddress),
                DefaultFromDisplayName = await SettingManager.GetSettingValueAsync(EmailSettingNames.DefaultFromDisplayName),
                SmtpHost = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Host),
                SmtpPort = await SettingManager.GetSettingValueAsync<int>(EmailSettingNames.Smtp.Port),
                SmtpUserName = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.UserName),
                SmtpPassword = SimpleStringCipher.Instance.Decrypt(smtpPassword),
                SmtpDomain = await SettingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Domain),
                SmtpEnableSsl = await SettingManager.GetSettingValueAsync<bool>(EmailSettingNames.Smtp.EnableSsl),
                SmtpUseDefaultCredentials = await SettingManager.GetSettingValueAsync<bool>(EmailSettingNames.Smtp.UseDefaultCredentials)
            };
        }

        private async Task<SecuritySettingsEditDto> GetSecuritySettingsAsync()
        {
            var passwordComplexitySetting = new PasswordComplexitySetting
            {
                //RequireDigit = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireDigit),
                //RequireLowercase = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireLowercase),
                //RequireNonAlphanumeric = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireNonAlphanumeric),
                //RequireUppercase = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireUppercase),
                //RequiredLength = await SettingManager.GetSettingValueAsync<int>(AbpZeroSettingNames.UserManagement.PasswordComplexity.RequiredLength)

                RequireDigit = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.PasswordComplexity.RequireDigit),
                RequireLowercase = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.PasswordComplexity.RequireLowercase),
                RequireNonAlphanumeric = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.PasswordComplexity.RequireNonAlphanumeric),
                RequireUppercase = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.PasswordComplexity.RequireUppercase),
                RequiredLength = await SettingManager.GetSettingValueAsync<int>(AppSettingNames.UserManagement.PasswordComplexity.RequiredLength)
            };

            var defaultPasswordComplexitySetting = new PasswordComplexitySetting
            {
                //RequireDigit = Convert.ToBoolean(_settingDefinitionManager.GetSettingDefinition(AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireDigit).DefaultValue),
                //RequireLowercase = Convert.ToBoolean(_settingDefinitionManager.GetSettingDefinition(AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireLowercase).DefaultValue),
                //RequireNonAlphanumeric = Convert.ToBoolean(_settingDefinitionManager.GetSettingDefinition(AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireNonAlphanumeric).DefaultValue),
                //RequireUppercase = Convert.ToBoolean(_settingDefinitionManager.GetSettingDefinition(AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireUppercase).DefaultValue),
                //RequiredLength = Convert.ToInt32(_settingDefinitionManager.GetSettingDefinition(AbpZeroSettingNames.UserManagement.PasswordComplexity.RequiredLength).DefaultValue)

                RequireDigit = Convert.ToBoolean(_settingDefinitionManager.GetSettingDefinition(AppSettingNames.UserManagement.PasswordComplexity.RequireDigit).DefaultValue),
                RequireLowercase = Convert.ToBoolean(_settingDefinitionManager.GetSettingDefinition(AppSettingNames.UserManagement.PasswordComplexity.RequireLowercase).DefaultValue),
                RequireNonAlphanumeric = Convert.ToBoolean(_settingDefinitionManager.GetSettingDefinition(AppSettingNames.UserManagement.PasswordComplexity.RequireNonAlphanumeric).DefaultValue),
                RequireUppercase = Convert.ToBoolean(_settingDefinitionManager.GetSettingDefinition(AppSettingNames.UserManagement.PasswordComplexity.RequireUppercase).DefaultValue),
                RequiredLength = Convert.ToInt32(_settingDefinitionManager.GetSettingDefinition(AppSettingNames.UserManagement.PasswordComplexity.RequiredLength).DefaultValue)
            };

            return new SecuritySettingsEditDto
            {
                UseDefaultPasswordComplexitySettings = passwordComplexitySetting.Equals(defaultPasswordComplexitySetting),
                PasswordComplexity = passwordComplexitySetting,
                DefaultPasswordComplexity = defaultPasswordComplexitySetting,
                UserLockOut = await GetUserLockOutSettingsAsync(),
                TwoFactorLogin = await GetTwoFactorLoginSettingsAsync()
            };
        }

        private async Task<HostBillingSettingsEditDto> GetBillingSettingsAsync()
        {
            return new HostBillingSettingsEditDto
            {
                LegalName = await SettingManager.GetSettingValueAsync(AppSettingNames.HostManagement.BillingLegalName),
                Address = await SettingManager.GetSettingValueAsync(AppSettingNames.HostManagement.BillingAddress)
            };
        }

        private async Task<UserLockOutSettingsEditDto> GetUserLockOutSettingsAsync()
        {
            return new UserLockOutSettingsEditDto
            {
                //IsEnabled = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.UserLockOut.IsEnabled),
                //MaxFailedAccessAttemptsBeforeLockout = await SettingManager.GetSettingValueAsync<int>(AbpZeroSettingNames.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout),
                //DefaultAccountLockoutSeconds = await SettingManager.GetSettingValueAsync<int>(AbpZeroSettingNames.UserManagement.UserLockOut.DefaultAccountLockoutSeconds)

                IsEnabled = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.UserLockOut.IsEnabled),
                MaxFailedAccessAttemptsBeforeLockout = await SettingManager.GetSettingValueAsync<int>(AppSettingNames.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout),
                DefaultAccountLockoutSeconds = await SettingManager.GetSettingValueAsync<int>(AppSettingNames.UserManagement.UserLockOut.DefaultAccountLockoutSeconds)
            };
        }

        private async Task<TwoFactorLoginSettingsEditDto> GetTwoFactorLoginSettingsAsync()
        {
            var twoFactorLoginSettingsEditDto = new TwoFactorLoginSettingsEditDto
            {
                //IsEnabled = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsEnabled),
                //IsEmailProviderEnabled = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsEmailProviderEnabled),
                //IsSmsProviderEnabled = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsSmsProviderEnabled),
                //IsRememberBrowserEnabled = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsRememberBrowserEnabled),
                //IsGoogleAuthenticatorEnabled = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.TwoFactorLogin.IsGoogleAuthenticatorEnabled)

                IsEnabled = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.TwoFactorLogin.IsEnabled),
                IsEmailProviderEnabled = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.TwoFactorLogin.IsEmailProviderEnabled),
                IsSmsProviderEnabled = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.TwoFactorLogin.IsSmsProviderEnabled),
                IsRememberBrowserEnabled = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.TwoFactorLogin.IsRememberBrowserEnabled),
                IsGoogleAuthenticatorEnabled = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.TwoFactorLogin.IsGoogleAuthenticatorEnabled)
            };
            return twoFactorLoginSettingsEditDto;
        }

        #endregion

        #region Update Settings

        public async Task UpdateAllSettings(HostSettingsEditDto input)
        {
            if (input.General != null) await UpdateGeneralSettingsAsync(input.General);
            if (input.TenantManagement != null) await UpdateTenantManagementAsync(input.TenantManagement);
            if (input.UserManagement != null) await UpdateUserManagementSettingsAsync(input.UserManagement);
            if (input.Security != null) await UpdateSecuritySettingsAsync(input.Security);
            if (input.Email != null) await UpdateEmailSettingsAsync(input.Email);
            if (input.Billing != null) await UpdateBillingSettingsAsync(input.Billing);
        }

        private async Task UpdateBillingSettingsAsync(HostBillingSettingsEditDto input)
        {
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.HostManagement.BillingLegalName, input.LegalName);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.HostManagement.BillingAddress, input.Address);

        }

        private async Task UpdateGeneralSettingsAsync(GeneralSettingsEditDto settings)
        {
            if (Clock.SupportsMultipleTimezone)
            {
                if (settings.Timezone.IsNullOrEmpty())
                {
                    var defaultValue = await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.Application, AbpSession.TenantId);
                    await SettingManager.ChangeSettingForApplicationAsync(TimingSettingNames.TimeZone, defaultValue);
                }
                else
                {
                    await SettingManager.ChangeSettingForApplicationAsync(TimingSettingNames.TimeZone, settings.Timezone);
                }
            }
        }

        private async Task UpdateTenantManagementAsync(TenantManagementSettingsEditDto settings)
        {
            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.TenantManagement.AllowSelfRegistration,
                settings.AllowSelfRegistration.ToString().ToLowerInvariant()
            );
            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.TenantManagement.IsNewRegisteredTenantActiveByDefault,
                settings.IsNewRegisteredTenantActiveByDefault.ToString().ToLowerInvariant()
            );

            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.TenantManagement.UseCaptchaOnRegistration,
                settings.UseCaptchaOnRegistration.ToString().ToLowerInvariant()
            );

            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.TenantManagement.DefaultEdition,
                settings.DefaultEditionId != null && settings.DefaultEditionId > 0 ? settings.DefaultEditionId.ToString() : ""
            );
        }

        private async Task UpdateUserManagementSettingsAsync(HostUserManagementSettingsEditDto settings)
        {
            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin,
                settings.IsEmailConfirmationRequiredForLogin.ToString().ToLowerInvariant()
            );
            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.UserManagement.SmsVerificationEnabled,
                settings.SmsVerificationEnabled.ToString().ToLowerInvariant()
            );
        }

        private async Task UpdateSecuritySettingsAsync(SecuritySettingsEditDto settings)
        {
            if (settings.UseDefaultPasswordComplexitySettings)
            {
                await UpdatePasswordComplexitySettingsAsync(settings.DefaultPasswordComplexity);
            }
            else
            {
                await UpdatePasswordComplexitySettingsAsync(settings.PasswordComplexity);
            }

            await UpdateUserLockOutSettingsAsync(settings.UserLockOut);
            await UpdateTwoFactorLoginSettingsAsync(settings.TwoFactorLogin);
        }

        private async Task UpdatePasswordComplexitySettingsAsync(PasswordComplexitySetting settings)
        {

            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.UserManagement.PasswordComplexity.RequireDigit,
                settings.RequireDigit.ToString()
            );

            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.UserManagement.PasswordComplexity.RequireLowercase,
                settings.RequireLowercase.ToString()
            );

            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.UserManagement.PasswordComplexity.RequireNonAlphanumeric,
                settings.RequireNonAlphanumeric.ToString()
            );

            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.UserManagement.PasswordComplexity.RequireUppercase,
                settings.RequireUppercase.ToString()
            );

            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.UserManagement.PasswordComplexity.RequiredLength,
                settings.RequiredLength.ToString()
            );
        }

        private async Task UpdateUserLockOutSettingsAsync(UserLockOutSettingsEditDto settings)
        {
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.UserManagement.UserLockOut.IsEnabled, settings.IsEnabled.ToString().ToLowerInvariant());
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.UserManagement.UserLockOut.DefaultAccountLockoutSeconds, settings.DefaultAccountLockoutSeconds.ToString());
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout, settings.MaxFailedAccessAttemptsBeforeLockout.ToString());
        }

        private async Task UpdateTwoFactorLoginSettingsAsync(TwoFactorLoginSettingsEditDto settings)
        {
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.UserManagement.TwoFactorLogin.IsEnabled, settings.IsEnabled.ToString().ToLowerInvariant());
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.UserManagement.TwoFactorLogin.IsEmailProviderEnabled, settings.IsEmailProviderEnabled.ToString().ToLowerInvariant());
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.UserManagement.TwoFactorLogin.IsSmsProviderEnabled, settings.IsSmsProviderEnabled.ToString().ToLowerInvariant());
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.UserManagement.TwoFactorLogin.IsGoogleAuthenticatorEnabled, settings.IsGoogleAuthenticatorEnabled.ToString().ToLowerInvariant());
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.UserManagement.TwoFactorLogin.IsRememberBrowserEnabled, settings.IsRememberBrowserEnabled.ToString().ToLowerInvariant());
        }

        private async Task UpdateEmailSettingsAsync(EmailSettingsEditDto settings)
        {
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.DefaultFromAddress, settings.DefaultFromAddress);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.DefaultFromDisplayName, settings.DefaultFromDisplayName);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Host, settings.SmtpHost);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Port, settings.SmtpPort.ToString(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.UserName, settings.SmtpUserName);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Password, SimpleStringCipher.Instance.Encrypt(settings.SmtpPassword));
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.Domain, settings.SmtpDomain);
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.EnableSsl, settings.SmtpEnableSsl.ToString().ToLowerInvariant());
            await SettingManager.ChangeSettingForApplicationAsync(EmailSettingNames.Smtp.UseDefaultCredentials, settings.SmtpUseDefaultCredentials.ToString().ToLowerInvariant());
        }

        #endregion

        public async Task<ListResultDto<string>> GetTimeZones(PagedFilterInputDto input)
        {
            var timezones = new List<string>();           
            await Task.Run(() => { 
                timezones = TimezoneHelper.GetWindowsTimeZoneIds()
                            .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), s => s.ToLower().Contains(input.Keyword.ToLower()))
                            .Skip(input.SkipCount)
                            .Take(input.MaxResultCount)
                            .ToList(); 
            });
            return new ListResultDto<string> { Items = timezones };
        }

    }
}