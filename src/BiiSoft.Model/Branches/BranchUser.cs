using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Authorization.Users;
using BiiSoft.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Branches
{
    [Table("BiiBranchUsers")]
    public class BranchUser : DefaultEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long MemberId { get; private set; }
        public User Member { get; private set; }
        public Guid BranchId { get; private set; }
        public Branch Branch { get; private set; }

        public static BranchUser Create(int tenantId, long userId, long memberId, Guid branchId)
        {
            return new BranchUser
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                MemberId = memberId,
                BranchId = branchId
            };
        }

        public void Update(long userId, long memberId, Guid branchId)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            MemberId = memberId;
            BranchId = branchId;
        }
    }
}
