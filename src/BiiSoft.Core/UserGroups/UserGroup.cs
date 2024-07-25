using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Timing;
using System.ComponentModel.DataAnnotations.Schema;
using BiiSoft.Entities;
using BiiSoft.Branches;

namespace BiiSoft.UserGroups
{
    [Table("BiiUserGroups")]
    public class UserGroup : CanModifyDefaultDescriptionNameEntity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public static UserGroup Create(int? tenantId, long userId, string name, string displayNmae, string description)
        {
            return new UserGroup
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayNmae,
                Description = description,
                IsActive = true
            };
        }

        public void Update(long userId, string name, string displayNmae, string description)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Name = name;
            DisplayName = displayNmae;
            Description = description;
        }

    }
}
