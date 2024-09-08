using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Dtos
{  
    public abstract class NavigationDto<TPrimary> : IdentityDot<TPrimary>, INavigationDto<TPrimary> where TPrimary : struct
    {
        public TPrimary? FirstId { get; set; }
        public TPrimary? NextId { get; set; }
        public TPrimary? PreviousId { get; set; }
        public TPrimary? LastId { get; set; }
    }

    public abstract class CreatorAuditedNavigationDto<TPrimary> : NavigationDto<TPrimary>, ICreatorAuditedDto where TPrimary : struct
    {
        public long? CreatorUserId { get; set; }
        public string CreatorUserName { get; set; }
        public DateTime? CreationTime { get; set; }
    }

    public abstract class AuditedNavigationDto<TPrimary> : CreatorAuditedNavigationDto<TPrimary>, IModifierAuditedDto where TPrimary : struct
    {
        public long? LastModifierUserId { get; set; }
        public string LastModifierUserName { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }

    public abstract class ActiveAuditedNavigationDto<TPrimary> : AuditedNavigationDto<TPrimary>, IActiveDto where TPrimary : struct
    {
        public bool IsActive { get; set; }
    }

    public abstract class NameActiveAuditedNavigationDto<TPrimary> : ActiveAuditedNavigationDto<TPrimary>, INameDto where TPrimary : struct
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public abstract class DefaultNameActiveAuditedNavigationDto<TPrimary> : NameActiveAuditedNavigationDto<TPrimary>, IDefaultDto where TPrimary : struct
    {
        public bool IsDefault { get; set; }
    }

    public abstract class CanModifyNameActiveAuditedNavigationDto<TPrimary> : NameActiveAuditedNavigationDto<TPrimary>, ICanModifyDto where TPrimary : struct
    {
        public bool CannotEdit { get; set; }
        public bool CannotDelete { get; set; }
    }

    public abstract class CanModifyDefaultNameActiveAuditedNavigationDto<TPrimary> : CanModifyNameActiveAuditedNavigationDto<TPrimary>, IDefaultDto where TPrimary : struct
    {
        public bool IsDefault { get; set; }
    }

}
