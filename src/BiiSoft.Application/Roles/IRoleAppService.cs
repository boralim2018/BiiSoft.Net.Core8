using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.Dtos;
using BiiSoft.Roles.Dto;

namespace BiiSoft.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PageRoleInputDto, CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();

        Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input);

        Task<ListResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input);
    }
}
