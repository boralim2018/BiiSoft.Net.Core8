using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.Authorization.Users.Dto;
using BiiSoft.Roles.Dto;
using BiiSoft.Users.Dto;

namespace BiiSoft.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserInputDto, CreateUserDto, UserDto>
    {
        Task Deactivate(EntityDto<long> user);
        Task Activate(EntityDto<long> user);
        Task<ListResultDto<RoleDto>> GetRoles();

        Task<bool> ChangePassword(ChangePasswordDto input);

        Task<PagedResultDto<UserSummaryDto>> FindUsers(FindUsersInput input);
        Task<bool> ResetPassword(ResetPasswordDto input);
        Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(EntityDto<long> input);
        Task UpdateUserPermissions(UpdateUserPermissionsInput input);
        Task ResetUserSpecificPermissions(EntityDto<long> input);
        Task Enable(EntityDto<long> input);
        Task Disable(EntityDto<long> input);

    }
}
