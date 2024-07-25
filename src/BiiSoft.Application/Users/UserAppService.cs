using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using BiiSoft.Authorization;
using BiiSoft.Authorization.Roles;
using BiiSoft.Authorization.Users;
using BiiSoft.Roles.Dto;
using BiiSoft.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Abp.Net.Mail;
using Abp.Runtime.Security;
using BiiSoft.Enums;
using BiiSoft.Authorization.Permissions;
using BiiSoft.Authorization.Users.Dto;
using Abp.Timing;
using BiiSoft.Validation;
using Abp.Authorization.Users;

namespace BiiSoft.Users
{
    [AbpAuthorize(PermissionNames.Pages_Administrations_Users, PermissionNames.Pages_Find_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedUserInputDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;
        //private readonly IUserEmailer _userEmailer;
        private readonly IEmailSender _emailSender;
        private readonly IUserPolicy _userPolicy;
        private readonly IPermissionManager _permissionManager;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            //IUserEmailer userEmailer,
            IEmailSender emailSender,
            IUserPolicy userPolicy,
            IPermissionManager permissionManager,
            LogInManager logInManager)
            : base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;
            //_userEmailer = userEmailer;
            _emailSender = emailSender;
            _userPolicy = userPolicy;
            _permissionManager = permissionManager;
        }

        
        [AbpAuthorize(PermissionNames.Pages_Administrations_Users_View, PermissionNames.Pages_Administrations_Users_Edit)]
        public override async Task<UserDto> GetAsync(EntityDto<long> input)
        {
            var result = await base.GetAsync(input);

            return result;
        }

        [AbpAuthorize(PermissionNames.Pages_Find_Users)]
        public async Task<PagedResultDto<UserSummaryDto>> FindUsers(FindUsersInput input)
        {
            if (AbpSession.TenantId.HasValue)
            {
                //Prevent tenants to get other tenant's users.
                input.TenantId = AbpSession.TenantId;
            }

            using (CurrentUnitOfWork.SetTenantId(input.TenantId))
            {
                var query = _userManager.Users
                    .WhereIf(input.IsActive.HasValue, s => s.IsActive == input.IsActive)
                    .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), u =>
                        u.Name.Contains(input.Keyword) ||
                        u.Surname.Contains(input.Keyword) ||
                        u.UserName.Contains(input.Keyword) ||
                        u.EmailAddress.Contains(input.Keyword)
                    )
                    .Select(s => new UserSummaryDto
                    {
                        Id = s.Id,
                        UserName = s.UserName,
                        FullName = s.FullName,
                        UseEmail = s.UseEmail,
                        EmailAddress = s.EmailAddress                       
                    });

                var userCount = await query.CountAsync();
                var users = new List<UserSummaryDto>();

                if (userCount > 0)
                {
                    query = query.OrderBy(input.GetOrdering());
                    if (input.UsePagination) query = query.PageBy(input);
                    users = await query.ToListAsync();
                }

                return new PagedResultDto<UserSummaryDto>(userCount, users);
            }
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_Users)]
        public override async Task<PagedResultDto<UserDto>> GetAllAsync(PagedUserInputDto input)
        {
            var query = this.Repository.GetAll()
                            .WhereIf(input.IsActive != null, s => s.IsActive == input.IsActive)
                            .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), s => 
                                s.UserName.ToLower().Contains(input.Keyword.ToLower()) ||
                                s.Surname.ToLower().Contains(input.Keyword.ToLower()) ||
                                s.Name.ToLower().Contains(input.Keyword.ToLower()) ||
                                s.EmailAddress.ToLower().Contains(input.Keyword.ToLower()))
                            .AsNoTracking()
                            .Select(s => new UserDto
                            {
                                Id = s.Id,
                                Name = s.Name,
                                Surname = s.Surname,
                                FullName = s.FullName,
                                UserName = s.UserName,
                                EmailAddress = s.EmailAddress,
                                IsActive = s.IsActive,
                                IsDeactivate = s.IsDeactivate,
                            });

            var totalCount = await query.CountAsync();
            var items = new List<UserDto>();

            if (totalCount > 0)
            {
                query = query.OrderBy(input.GetOrdering());
                if (input.UsePagination) query = query.PageBy(input);
                items = await query.ToListAsync();
            }

            return new PagedResultDto<UserDto>(totalCount, items);
        }

        private string ValidateEmail(string userName, bool useEmail, string emailAddress)
        {
            if (!useEmail) emailAddress = $"{userName.ToLower()}@noemail.com";
            if(!ValidationHelper.IsEmail(emailAddress)) throw new UserFriendlyException(L("InvalidEmailAddress") + $" {emailAddress}");
            if (emailAddress.Length > AbpUserBase.MaxEmailAddressLength) throw new UserFriendlyException(L("CannotBeGreaterThan", L("EmailAddress"), $"{AbpUserBase.MaxEmailAddressLength} {L("Characters").ToLower()}"));
            return emailAddress;
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_Users_Create)]
        public override async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            if (AbpSession.TenantId.HasValue)
            {
                await _userPolicy.CheckMaxUserCountAsync(AbpSession.GetTenantId());
            }

            input.EmailAddress = ValidateEmail(input.UserName, input.UseEmail, input.EmailAddress);

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            CheckErrors(await _userManager.CreateAsync(user, input.Password));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();


            return MapToEntityDto(user);
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_Users_Edit)]
        public override async Task<UserDto> UpdateAsync(UserDto input)
        {

            var user = await _userManager.GetUserByIdAsync(input.Id);

            input.EmailAddress = ValidateEmail(input.UserName, input.UseEmail, input.EmailAddress);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
            }

            return await GetAsync(input);
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_Users_Delete)]
        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }


        [AbpAuthorize(PermissionNames.Pages_Administrations_Users_Enable)]
        public async Task Enable(EntityDto<long> input)
        {
            var entity = await Repository.FirstOrDefaultAsync(s => s.Id == input.Id);
            if (entity == null) throw new UserFriendlyException(L("RecordNotFound"));

            entity.IsActive = true;
            entity.LastModificationTime = Clock.Now;
            entity.LastModifierUserId = AbpSession.UserId;

            await _userManager.UpdateAsync(entity);
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_Users_Disable)]
        public async Task Disable(EntityDto<long> input)
        {
            var entity = await Repository.FirstOrDefaultAsync(s => s.Id == input.Id);
            if (entity == null) throw new UserFriendlyException(L("RecordNotFound"));

            entity.IsActive = false;
            entity.LastModificationTime = Clock.Now;
            entity.LastModifierUserId = AbpSession.UserId;

            await _userManager.UpdateAsync(entity);
        }


        [AbpAuthorize(PermissionNames.Pages_Administrations_Users_Activate)]
        public async Task Activate(EntityDto<long> input)
        {
            if (AbpSession.TenantId.HasValue)
            {
                await _userPolicy.CheckMaxUserCountAsync(AbpSession.GetTenantId());
            }

            var entity = await Repository.FirstOrDefaultAsync(s => s.Id == input.Id);
            if (entity == null) throw new UserFriendlyException(L("RecordNotFound"));

            entity.IsDeactivate = false;
            entity.IsActive = true;
            entity.LastModificationTime = Clock.Now;
            entity.LastModifierUserId = AbpSession.UserId;

            await _userManager.UpdateAsync(entity);
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_Users_Deactivate)]
        public async Task Deactivate(EntityDto<long> input)
        {
            var entity = await Repository.FirstOrDefaultAsync(s => s.Id == input.Id);
            if (entity == null) throw new UserFriendlyException(L("RecordNotFound"));

            entity.IsDeactivate = true;
            entity.IsActive = false;
            entity.LastModificationTime = Clock.Now;
            entity.LastModifierUserId = AbpSession.UserId;

            await _userManager.UpdateAsync(entity);
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roleIds = user.Roles.Select(x => x.RoleId).ToArray();

            var roles = _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.NormalizedName);

            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();

            return userDto;
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedUserInputDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.EmailAddress.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserInputDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<bool> ChangePassword(ChangePasswordDto input)
        {
            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }
            
            if (await _userManager.CheckPasswordAsync(user, input.CurrentPassword))
            {
                CheckErrors(await _userManager.ChangePasswordAsync(user, input.NewPassword));
            }
            else
            {
                CheckErrors(IdentityResult.Failed(new IdentityError
                {
                    Description = "Incorrect password."
                }));
            }

            return true;
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_Users_ResetPassword)]
        public async Task<bool> ResetPassword(ResetPasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attempting to reset password.");
            }
            
            var currentUser = await _userManager.GetUserByIdAsync(_abpSession.GetUserId());
            var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
            }
            
            if (currentUser.IsDeleted || !currentUser.IsActive)
            {
                return false;
            }
            
            var roles = await _userManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                throw new UserFriendlyException("Only administrators may reset passwords.");
            }

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            return true;
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_Users_ChangePermissions)]
        public async Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            var permissions = PermissionManager.GetAllPermissions();
            var grantedPermissions = await _userManager.GetGrantedPermissionsAsync(user);

            var permissionList = ObjectMapper.Map<List<PermissionDto>>(permissions).OrderBy(s => s.Parent == null ? "" : s.Parent.Name).ThenBy(p => p.Name).ToList();

            return new GetUserPermissionsForEditOutput
            {
                Permissions = permissionList,
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_Users_ChangePermissions)]
        public async Task UpdateUserPermissions(UpdateUserPermissionsInput input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            var grantedPermissions = _permissionManager.GetPermissionsFromNamesByValidating(input.GrantedPermissionNames);
            await _userManager.SetGrantedPermissionsAsync(user, grantedPermissions);
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_Users_ChangePermissions)]
        public async Task ResetUserSpecificPermissions(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.ResetAllPermissionsAsync(user);
        }
    }
}

