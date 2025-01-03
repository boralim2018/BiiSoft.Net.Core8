using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Warehouses;
using BiiSoft.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Items
{
    [Table("BiiItemZones")]
    public class ItemZone : DefaultEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public Guid ItemId { get; private set; }
        public Item Item { get; private set; }
        public Guid ZoneId { get; private set; }
        public Zone Zone { get; private set; }

        public static ItemZone Create(int tenantId, long userId, Guid itemId, Guid zoneId)
        {
            return new ItemZone
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                ItemId = itemId,
                ZoneId = zoneId
            };
        }

        public void Update(long userId, Guid itemId, Guid zoneId)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            ItemId = itemId;
            ZoneId = zoneId;
        }
    }
}
