using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using BiiSoft.Authorization.Users;
using BiiSoft.MultiTenancy;
using Abp.Dependency;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Abp.Extensions;
using Microsoft.Extensions.Configuration;
using BiiSoft.Configuration;
using Abp.Threading;
using System.Globalization;
using Abp.Localization;

namespace BiiSoft
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class BiiSoftAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        private readonly IApplicationLanguageManager _applicationLanguageManager;

        //protected readonly IConfigurationRoot _appConfig;

        protected BiiSoftAppServiceBase()
        {
            LocalizationSourceName = BiiSoftConsts.LocalizationSourceName;

            //var env = IocManager.Instance.Resolve<IWebHostEnvironment>();
            //_appConfig = env.GetAppConfiguration();
            //SERVER_ROOT = env.WebRootPath.Replace("\\", "/").EnsureEndsWith('/');

            //var httpContextAccessor = IocManager.Instance.Resolve<IHttpContextAccessor>();
            //BASE_URL = httpContextAccessor.HttpContext.Request.Host.Value.EnsureEndsWith('/');

            _applicationLanguageManager = IocManager.Instance.Resolve<ApplicationLanguageManager>();       
        }

        protected async Task<bool> IsDefaultLagnuageAsync()
        {            
            var defaultLanguage = await _applicationLanguageManager.GetDefaultLanguageOrNullAsync(AbpSession.TenantId);
            return defaultLanguage != null && defaultLanguage.Name == CultureInfo.CurrentCulture.Name;
        }

        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            using (CurrentUnitOfWork.SetTenantId(null))
            {
                return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
            }
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
