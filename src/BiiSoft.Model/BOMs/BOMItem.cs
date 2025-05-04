using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Entities;
using BiiSoft.Items;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.BOMs
{
    [Table("BiiBOMItems")]
    public class BOMItem : BaseAuditedEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public Guid BOMId { get; private set; }
        public BOM BOM { get; private set; }
        public Guid ItemId { get; private set; }
        public Item Item { get; private set; }
        public decimal Qty { get; private set; }

        public static BOMItem Create(int tenantId, long userId, Guid bomId, Guid itemId, decimal qty)
        {
            return new BOMItem
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                BOMId = bomId,
                ItemId = itemId,
                Qty = qty,
            };
        }

        public void Update(long userId, Guid itemId, decimal qty)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            ItemId = itemId;
            Qty = qty;
        }
    }
}
