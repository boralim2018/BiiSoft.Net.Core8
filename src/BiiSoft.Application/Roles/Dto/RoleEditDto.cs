using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Roles;
using BiiSoft.Authorization.Roles;

namespace BiiSoft.Roles.Dto
{
    public class RoleEditDto: EntityDto<int>
    {
        [Required]
        [StringLength(AbpRoleBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpRoleBase.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        [StringLength(BiiSoftConsts.MaxLengthDescription, ErrorMessage = BiiSoftConsts.MaxLengthDescriptionErrorMessage)]
        public string Description { get; set; }

        public bool IsStatic { get; set; }
    }
}