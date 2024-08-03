using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using BiiSoft.Authorization;
using BiiSoft.Authorization.Roles;
using BiiSoft.Authorization.Users;
using BiiSoft.Roles.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace BiiSoft.Roles
{
    [AbpAuthorize(PermissionNames.Pages_Administrations_Roles)]
    public class RoleAppService : AsyncCrudAppService<Role, RoleDto, int, PageRoleInputDto, CreateRoleDto, RoleDto>, IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;

        public RoleAppService(IRepository<Role> repository, RoleManager roleManager, UserManager userManager)
            : base(repository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_Roles_Create)]
        public override async Task<RoleDto> CreateAsync(CreateRoleDto input)
        {
            CheckCreatePermission();

            var role = ObjectMapper.Map<Role>(input);
            role.SetNormalizedName();

            CheckErrors(await _roleManager.CreateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.GrantedPermissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_Roles)]
        public async Task<ListResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input)
        {           

            var roles = await _roleManager
                .Roles
                .WhereIf(
                    !input.Permission.IsNullOrWhiteSpace(),
                    r => r.Permissions.Any(rp => rp.Name == input.Permission && rp.IsGranted)
                )
                .ToListAsync();

            return new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(roles));
        }

        public override async Task<PagedResultDto<RoleDto>> GetAllAsync(PageRoleInputDto input)
        {
            var query = this.Repository.GetAll()
                            .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), s => s.Name.ToLower().Contains(input.Keyword.ToLower()))
                            .AsNoTracking()
                            .Select(s => new RoleDto
                            {
                                Id = s.Id,
                                Name = s.Name,
                                Description = s.Description,
                                DisplayName = s.DisplayName                               
                            });

            var totalCount = await query.CountAsync();
            var items = new List<RoleDto>();

            if (totalCount > 0)
            {
                query = query.OrderBy(input.GetOrdering());
                if (input.UsePagination) query = query.PageBy(input);
                items = await query.ToListAsync();
            }

            return new PagedResultDto<RoleDto>(totalCount, items);
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_Roles_Edit)]
        public override async Task<RoleDto> UpdateAsync(RoleDto input)
        {
            CheckUpdatePermission();

            var role = await _roleManager.GetRoleByIdAsync(input.Id);

            ObjectMapper.Map(input, role);

            CheckErrors(await _roleManager.UpdateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.GrantedPermissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }

        [AbpAuthorize(PermissionNames.Pages_Administrations_Roles_Delete)]
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();

            var role = await _roleManager.FindByIdAsync(input.Id.ToString());
            var users = await _userManager.GetUsersInRoleAsync(role.NormalizedName);

            foreach (var user in users)
            {
                CheckErrors(await _userManager.RemoveFromRoleAsync(user, role.NormalizedName));
            }

            CheckErrors(await _roleManager.DeleteAsync(role));
        }

        public Task<ListResultDto<PermissionDto>> GetAllPermissions()
        {
            var permissions = PermissionManager.GetAllPermissions();

            return Task.FromResult(new ListResultDto<PermissionDto>(
                         ObjectMapper.Map<List<PermissionDto>>(permissions)
                         .OrderBy(s => s.Parent == null ? "" : s.Parent.Name)
                         .ThenBy(p => p.Name).ToList()));
        }

        protected override IQueryable<Role> CreateFilteredQuery(PageRoleInputDto input)
        {
            return Repository.GetAllIncluding(x => x.Permissions)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword)
                || x.DisplayName.Contains(input.Keyword)
                || x.Description.Contains(input.Keyword));
        }

        protected override async Task<Role> GetEntityByIdAsync(int id)
        {
            return await Repository.GetAllIncluding(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == id);
        }

        protected override IQueryable<Role> ApplySorting(IQueryable<Role> query, PageRoleInputDto input)
        {
            return query.OrderBy(r => r.DisplayName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input)
        {
            var permissions = PermissionManager.GetAllPermissions();
            var role = await _roleManager.GetRoleByIdAsync(input.Id);
            var grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
            var roleEditDto = ObjectMapper.Map<RoleEditDto>(role);

            return new GetRoleForEditOutput
            {
                Role = roleEditDto,
                Permissions = ObjectMapper.Map<List<PermissionDto>>(permissions).OrderBy(s => s.Parent == null ? "" : s.Parent.Name).ThenBy(p => p.Name).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }
    }
}

