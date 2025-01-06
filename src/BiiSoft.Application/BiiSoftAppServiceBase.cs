using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using BiiSoft.Authorization.Users;
using BiiSoft.MultiTenancy;
using Abp.Dependency;
using System.Globalization;
using Abp.Localization;
using BiiSoft.Entities;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using BiiSoft.Branches;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
        private readonly IBiiSoftRepository<CompanyAdvanceSetting, long> _companyAdvanceRepository;
        private readonly IBiiSoftRepository<Branch, Guid> _branchRepository;

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
            _companyAdvanceRepository = IocManager.Instance.Resolve<IBiiSoftRepository<CompanyAdvanceSetting, long>>();
            _branchRepository = IocManager.Instance.Resolve<IBiiSoftRepository<Branch, Guid>>();
        }

        protected async Task<CompanyAdvanceSetting> GetCompanyAdvanceSettingAsync()
        {
            return await _companyAdvanceRepository.GetAll().AsNoTracking().FirstOrDefaultAsync();
        }
        protected async Task<bool> GetMultiBranchEnableAsync()
        {
            return await _companyAdvanceRepository.GetAll().AsNoTracking().Select(s => s.MultiBranchesEnable).FirstOrDefaultAsync();
        }

        protected async Task<List<Guid>> GetUserBranchIdsAsync()
        {
            return await _branchRepository.GetAll().AsNoTracking()
                         .Where(s => 
                            s.Sharing == Enums.Sharing.All || 
                            s.BranchUsers.Any(r => r.MemberId == AbpSession.UserId))
                         .Select(s => s.Id)
                         .ToListAsync();
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

        protected TEntity MapEntity<TEntity, TPrimaryKey>(object input)
        {
            var entity = ObjectMapper.Map<TEntity>(input);

            if (entity is IUserEntity<TPrimaryKey>)
            {
                (entity as IUserEntity<TPrimaryKey>).UserId = AbpSession.UserId;
            }
            else if(entity is ICreationAudited)
            {
                (entity as ICreationAudited).CreatorUserId = AbpSession.UserId;
            }

            if (entity is IModificationAudited) 
            { 
                (entity as IModificationAudited).LastModifierUserId = AbpSession.UserId;
            }

            if(entity is IMayHaveTenant || entity is IMayHaveTenant)
            {
                (entity as IMayHaveTenant).TenantId = AbpSession.TenantId;
            }

            return entity;
        }

    }

}
