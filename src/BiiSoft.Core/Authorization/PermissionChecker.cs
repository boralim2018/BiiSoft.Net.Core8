using Abp.Authorization;
using BiiSoft.Authorization.Roles;
using BiiSoft.Authorization.Users;

namespace BiiSoft.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
