using System;

namespace BiiSoft.Dtos
{
    
    public abstract class CreatorAuditedDto<TPrimary> : IdentityDot<TPrimary>, ICreatorAuditedDto
    {   
        public long? CreatorUserId { get; set; }
        public string CreatorUserName { get; set; }
        public DateTime? CreationTime { get; set; }
    }

    public abstract class AuditedDto<TPrimary> : CreatorAuditedDto<TPrimary>, IModifierAuditedDto
    {
        public long? LastModifierUserId { get; set; }
        public string LastModifierUserName { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }

    public abstract class ActiveAuditedDto<TPrimary> : AuditedDto<TPrimary>, IActiveDto
    {
        public bool IsActive { get; set; }
    }

    public abstract class NameActiveAuditedDto<TPrimary> : ActiveAuditedDto<TPrimary>, INameDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public abstract class DefaultNameActiveAuditedDto<TPrimary> : NameActiveAuditedDto<TPrimary>, IDefaultDto
    {
        public bool IsDefault { get; set; }
    }

    public abstract class CanModifyNameActiveAuditedDto<TPrimary> : NameActiveAuditedDto<TPrimary>, ICanModifyDto
    {
        public bool CannotEdit { get; set; }
        public bool CannotDelete { get; set; }
    }

    public abstract class CanModifyDefaultNameActiveAuditedDto<TPrimary> : CanModifyNameActiveAuditedDto<TPrimary>, IDefaultDto
    {
        public bool IsDefault { get; set; }
    }

}
