using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Abp.UI;
using Abp.Zero.Configuration;
using BiiSoft.Authorization.Accounts.Dto;
using BiiSoft.Authorization.Users;
using BiiSoft.Url;
using Microsoft.AspNetCore.Hosting;
using BiiSoft.Configuration;
using Abp.Authorization;
using BiiSoft.MultiTenancy;
using BiiSoft.Authorization.Impersonation;
using Abp.Runtime.Session;
using System;
using Abp.Extensions;
using Abp.Runtime.Security;
using System.Web;
using Abp.Localization;
using BiiSoft.Users.Dto;
using BiiSoft.Security.Recaptcha;

namespace BiiSoft.Authorization.Accounts
{
    public class AccountAppService : BiiSoftAppServiceBase, IAccountAppService
    {
        public IAppUrlService AppUrlService { get; set; }

        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly IUserEmailer _userEmailer;        
        private readonly string AppName;
        private readonly IImpersonationManager _impersonationManager;
        private readonly IUserLinkManager _userLinkManager;
        private readonly IUserPolicy _userPolicy;
        public IRecaptchaValidator RecaptchaValidator { get; set; }

        public AccountAppService(
            IImpersonationManager impersonationManager,
            IUserLinkManager userLinkManager,
            UserRegistrationManager userRegistrationManager,
            IUserPolicy userPolicy,
            IUserEmailer userEmailer
            ): base()
        {
            _impersonationManager = impersonationManager;
            _userLinkManager = userLinkManager;
            _userRegistrationManager = userRegistrationManager;
            _userEmailer = userEmailer;
            _userPolicy = userPolicy;

            AppUrlService = NullAppUrlService.Instance;

            var env = IocManager.Instance.Resolve<IWebHostEnvironment>();
            var _appConfig = env.GetAppConfiguration();
            AppName = _appConfig["App:Name"];
            RecaptchaValidator = NullRecaptchaValidator.Instance;
        }

        public async Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input)
        {
            var tenant = await TenantManager.FindByTenancyNameAsync(input.TenancyName);
            if (tenant == null)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.NotFound);
            }

            if (!tenant.IsActive)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.InActive);
            }

            return new IsTenantAvailableOutput(TenantAvailabilityState.Available, tenant.Id);
        }

        public async Task<RegisterOutput> Register(RegisterInput input)
        {
            if (UseCaptchaOnLogin())
            {
                await RecaptchaValidator.ValidateAsync(input.CaptchaResponse);
            }

            if (AbpSession.TenantId.HasValue)
            {
                await _userPolicy.CheckMaxUserCountAsync(AbpSession.GetTenantId());
            }

            var user = await _userRegistrationManager.RegisterAsync(
                input.Name,
                input.Surname,
                input.EmailAddress,
                input.UserName,
                input.Password,
                true // Assumed email address is always confirmed. Change this if you want to implement email confirmation.
            );

            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

            return new RegisterOutput
            {
                CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
            };
        }

        public async Task<ResetPasswordOutput> ResetPassword(ResetPasswordInput input)
        {
            if (UseCaptchaOnLogin())
            {
                await RecaptchaValidator.ValidateAsync(input.CaptchaResponse);
            }

            var user = await UserManager.GetUserByIdAsync(input.UserId);
            if (user == null || user.PasswordResetCode.IsNullOrEmpty() || user.PasswordResetCode != input.ResetCode)
            {
                throw new UserFriendlyException(L("InvalidPasswordResetCode"), L("InvalidPasswordResetCode_Detail"));
            }

            await UserManager.InitializeOptionsAsync(AbpSession.TenantId);
            CheckErrors(await UserManager.ChangePasswordAsync(user, input.Password));
            user.PasswordResetCode = null;
            user.IsEmailConfirmed = true;
            user.ShouldChangePasswordOnNextLogin = false;

            await UserManager.UpdateAsync(user);

            return new ResetPasswordOutput
            {
                CanLogin = user.IsActive,
                UserName = user.UserName
            };
        }

        public Task<int?> ResolveTenantId(ResolveTenantIdInput input)
        {
            if (string.IsNullOrEmpty(input.c))
            {
                return Task.FromResult(AbpSession.TenantId);
            }

            var parameters = SimpleStringCipher.Instance.Decrypt(input.c);
            var query = HttpUtility.ParseQueryString(parameters);

            if (query["tenantId"] == null)
            {
                return Task.FromResult<int?>(null);
            }

            var tenantId = Convert.ToInt32(query["tenantId"]) as int?;
            return Task.FromResult(tenantId);
        }

        private bool UseCaptchaOnLogin()
        {
            //if (DebugHelper.IsDebug)
            //{
            //    return false;
            //}

            return SettingManager.GetSettingValue<bool>(AppSettingNames.UserManagement.UseCaptchaOnLogin);
        }

        public async Task SendPasswordResetCode(SendPasswordResetCodeInput input)
        {
            if (UseCaptchaOnLogin())
            {
                await RecaptchaValidator.ValidateAsync(input.CaptchaResponse);
            }

            var user = await GetUserByChecking(input.EmailAddress);
            user.SetNewPasswordResetCode();
            await UserManager.UpdateAsync(user);

            var link = AppUrlService.CreatePasswordResetUrlFormat(AbpSession.TenantId);
            await _userEmailer.SendPasswordResetLinkAsyncNew(AppName, user, link);
        }

        private async Task<User> GetUserByChecking(string inputEmailAddress)
        {
            var user = await UserManager.FindByEmailAsync(inputEmailAddress);
            if (user == null)
            {
                throw new UserFriendlyException(L("InvalidEmailAddress"));
            }

            return user;
        }

        #region Impersonate

        [AbpAuthorize(PermissionNames.Pages_Administrations_Users_Impersonation, PermissionNames.Pages_Tenants_Impersonation)]
        public virtual async Task<ImpersonateOutput> Impersonate(ImpersonateInput input)
        {
            return new ImpersonateOutput
            {
                ImpersonationToken = await _impersonationManager.GetImpersonationToken(input.UserId, input.TenantId),
                TenancyName = await GetTenancyNameOrNullAsync(input.TenantId)
            };
        }

        private async Task<string> GetTenancyNameOrNullAsync(int? tenantId)
        {
            return tenantId.HasValue ? (await GetActiveTenantAsync(tenantId.Value)).TenancyName : null;
        }

        private async Task<Tenant> GetActiveTenantAsync(int tenantId)
        {
            var tenant = await TenantManager.FindByIdAsync(tenantId);
            if (tenant == null)
            {
                throw new UserFriendlyException(L("UnknownTenantId{0}", tenantId));
            }

            if (!tenant.IsActive)
            {
                throw new UserFriendlyException(L("TenantIdIsNotActive{0}", tenantId));
            }

            return tenant;
        }

        public virtual async Task<ImpersonateOutput> BackToImpersonator()
        {
            return new ImpersonateOutput
            {
                ImpersonationToken = await _impersonationManager.GetBackToImpersonatorToken(),
                TenancyName = await GetTenancyNameOrNullAsync(AbpSession.ImpersonatorTenantId)
            };
        }

        #endregion

        #region Link Account
        public virtual async Task<SwitchToLinkedAccountOutput> SwitchToLinkedAccount(SwitchToLinkedAccountInput input)
        {
            if (!await _userLinkManager.AreUsersLinked(AbpSession.ToUserIdentifier(), input.ToUserIdentifier()))
            {
                throw new Exception(L("ThisAccountIsNotLinkedToYourAccount"));
            }

            return new SwitchToLinkedAccountOutput
            {
                SwitchAccountToken = await _userLinkManager.GetAccountSwitchToken(input.TargetUserId, input.TargetTenantId),
                TenancyName = await GetTenancyNameOrNullAsync(input.TargetTenantId)
            };
        }
        #endregion

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }
    }
}
