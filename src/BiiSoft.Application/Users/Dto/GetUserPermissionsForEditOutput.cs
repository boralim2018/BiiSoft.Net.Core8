using BiiSoft.Authorization.Permissions.Dto;
using BiiSoft.Roles.Dto;
using System.Collections.Generic;

namespace BiiSoft.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<PermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}