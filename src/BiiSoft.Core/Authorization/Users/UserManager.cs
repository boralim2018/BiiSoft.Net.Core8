using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Abp.Runtime.Caching;
using BiiSoft.Authorization.Roles;
using Abp.Authorization.Roles;
using Abp.Localization;
using System.Threading.Tasks;
using Abp;
using Abp.Runtime.Session;
using System.Security.Claims;
using System.Linq;

namespace BiiSoft.Authorization.Users
{
    public class UserManager : AbpUserManager<Role, User>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ILocalizationManager _localizationManager;
        private readonly ICacheManager _cacheManager;

        public UserManager(
             ILocalizationManager localizationManager,
          RoleManager roleManager,
          UserStore store,
          IOptions<IdentityOptions> optionsAccessor,
          IPasswordHasher<User> passwordHasher,
          IEnumerable<IUserValidator<User>> userValidators,
          IEnumerable<IPasswordValidator<User>> passwordValidators,
          ILookupNormalizer keyNormalizer,
          IdentityErrorDescriber errors,
          IServiceProvider services,
          ILogger<UserManager<User>> logger,
          IPermissionManager permissionManager,
          IUnitOfWorkManager unitOfWorkManager,
          ICacheManager cacheManager,
          IRepository<OrganizationUnit, long> organizationUnitRepository,
          IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
          IOrganizationUnitSettings organizationUnitSettings,
          ISettingManager settingManager, 
          IRepository<UserLogin, long> userLoginRepository)
          : base(
              roleManager,
              store,
              optionsAccessor,
              passwordHasher,
              userValidators,
              passwordValidators,
              keyNormalizer,
              errors,
              services,
              logger,
              permissionManager,
              unitOfWorkManager,
              cacheManager,
              organizationUnitRepository,
              userOrganizationUnitRepository,
              organizationUnitSettings,
              settingManager,
              userLoginRepository)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _localizationManager = localizationManager;
            _cacheManager = cacheManager;
        }

        [UnitOfWork]
        public virtual async Task<User> GetUserOrNullAsync(UserIdentifier userIdentifier)
        {
            using (_unitOfWorkManager.Current.SetTenantId(userIdentifier.TenantId))
            {
                return await FindByIdAsync(userIdentifier.UserId.ToString());
            }
        }

        public async Task<User> GetUserAsync(UserIdentifier userIdentifier)
        {
            var user = await GetUserOrNullAsync(userIdentifier);
            if (user == null)
            {
                throw new Exception("There is no user: " + userIdentifier);
            }

            return user;
        }

        public async Task Logout(ClaimsPrincipal User)
        {
            if (AbpSession.UserId != null)
            {
                var tokenValidityKeyInClaims = User.Claims.First(c => c.Type == BiiSoftConsts.TokenValidityKey);
                var user = await GetUserAsync(AbpSession.ToUserIdentifier());
                await RemoveTokenValidityKeyAsync(user, tokenValidityKeyInClaims.Value);
                _cacheManager.GetCache(BiiSoftConsts.TokenValidityKey).Remove(tokenValidityKeyInClaims.Value);
            }
        }

        private new string L(string name)
        {
            return _localizationManager.GetString(BiiSoftConsts.LocalizationSourceName, name);
        }
    }
}
