using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Entities;
using BiiSoft.Enums;
using BiiSoft.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.BOMs
{
    [Table("BiiBOMs")]
    public class BOM : DefaultNameActiveEntity<Guid>, IMustHaveTenant, INoEntity
    {
        public int TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
        public BOMType Type { get; private set; }
        public Guid ItemId { get; private set; }
        public Item Item { get; private set; }
        public ICollection<BOMItem> BOMItems { get; private set; }

        public static BOM Create(int tenantId, long userId, string name, string displayName, BOMType type, Guid itemId)
        {
            return new BOM
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Type = type,
                ItemId = itemId,
                IsActive = true
            };
        }

        public void Update(long userId, string name, string displayName, BOMType type, Guid itemId)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Name = name;
            DisplayName = displayName;
            Type = type;
            ItemId = itemId;
        }
    }
}
