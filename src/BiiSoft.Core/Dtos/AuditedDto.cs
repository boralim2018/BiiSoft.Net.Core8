using System;

namespace BiiSoft.Dtos
{
    public class IdentityDot<TPrimary>
    {
        public TPrimary Id { get; set; }
    }

    public class CreationAuditedDto<TPrimary> : IdentityDot<TPrimary>
    {   
        public long? CreatorUserId { get; set; }
        public string CreatorUserName { get; set; }
        public DateTime? CreationTime { get; set; }
    }

    public class AuditedDto<TPrimary> : CreationAuditedDto<TPrimary>
    {
        public long? LastModifierUserId { get; set; }
        public string LastModifierUserName { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }

    public class ActiveAuditedDto<TPrimary> : AuditedDto<TPrimary>
    {
        public bool IsActive { get; set; }
    }

    public class NameActiveAuditedDto<TPrimary> : ActiveAuditedDto<TPrimary>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public class CanModifyNameActiveAuditedDto<TPrimary> : NameActiveAuditedDto<TPrimary>
    {
        public bool CannotEdit { get; set; }
        public bool CannotDelete { get; set; }
    }

    public class DefaultNameActiveAuditedDto<TPrimary> : NameActiveAuditedDto<TPrimary>
    {
        public bool IsDefault { get; set; }
    }

    public class CanModifyDefaultNameActiveAuditedDto<TPrimary> : CanModifyNameActiveAuditedDto<TPrimary>
    {
        public bool IsDefault { get; set; }
    }

    public class ActiveDto<TPrimary> : IdentityDot<TPrimary>
    {
        public bool IsActive { get; set; }
    }

    public class NameDto<TPrimary> : IdentityDot<TPrimary>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public class NameDto : NameDto<int>
    {

    }

    public class NameActiveDto<TPrimary> : NameDto<TPrimary>
    {
        public bool IsActive { get; set; }
    }

    public class CanModifyNameDto<TPrimary> : NameActiveDto<TPrimary>
    {
        public bool CannotEdit { get; set; }
        public bool CannotDelete { get; set; }
    }

    public class DefaultNameDto<TPrimary> : NameActiveDto<TPrimary>
    {
        public bool IsDefault { get; set; }
    }

    public class CanModifyDefaultNameDto<TPrimary> : CanModifyNameDto<TPrimary>
    {
        public bool IsDefault { get; set; }
    }

}
