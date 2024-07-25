using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Roles;
using BiiSoft.Authorization.Users;

namespace BiiSoft.Authorization.Roles
{
    public class Role : AbpRole<User>
    {
        public Role()
        {
        }

        public Role(int? tenantId, string displayName)
            : base(tenantId, displayName)
        {

        }

        public Role(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {

        }

        [MaxLength(BiiSoftConsts.MaxLengthDescription)]
        [StringLength(BiiSoftConsts.MaxLengthDescription, ErrorMessage = BiiSoftConsts.MaxLengthDescriptionErrorMessage)]
        public string Description {get; set;}
    }
}
