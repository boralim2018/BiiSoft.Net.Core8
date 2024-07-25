using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Authorization.Users;
using BiiSoft.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.UserGroups
{
    [Table("BiiUserGroupMembers")]
    public class UserGroupMember : DefaultEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long MemberId { get; private set; }
        public User Member { get; private set; }
        public Guid UserGroupId { get; private set; }
        public UserGroup UserGroup { get; private set; }

        public static UserGroupMember Create(int tenantId, long userId, long memberId, Guid userGroupId)
        {
            return new UserGroupMember
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                MemberId = memberId,
                UserGroupId = userGroupId
            };
        }

        public void Update(long userId, long memberId, Guid userGroupId)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            MemberId = memberId;
            UserGroupId = userGroupId;
        }
    }
}
