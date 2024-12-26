using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using BiiSoft.Entities;

namespace BiiSoft.Items
{
    [Table("BiiItemSizes")]
    public class ItemSize : DefaultNameActiveEntity<Guid>, IMustHaveTenant, INoEntity
    {
        public int TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
        
        public static ItemSize Create(int tenantId, long userId, string name, string displayName)
        {
            return new ItemSize
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                IsActive = true
            };
        }

        public void Update(long userId, string name, string displayName)
        {
            this.LastModifierUserId = userId;
            this.LastModificationTime = Clock.Now;
            this.Name = name;
            this.DisplayName = displayName;
        }

    }
}
