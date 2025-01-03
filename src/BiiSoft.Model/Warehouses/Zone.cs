using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Warehouses
{
    [Table("BiiZones")]
    public class Zone : DefaultNameActiveEntity<Guid>, IMayHaveTenant, INoEntity
    {
        public int? TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }

        public Guid WarehouseId { get; private set; }
        public Warehouse Warehouse { get; private set; }

        public static Zone Create(int tenantId, long userId, Guid warehouseId, string name, string displayName)
        {
            return new Zone
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                WarehouseId = warehouseId,
                Name = name,
                DisplayName = displayName,
                IsActive = true
            };
        }

        public void Update(long userId, Guid warehouseId, string name, string displayName)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            WarehouseId = WarehouseId;
            Name = name;
            DisplayName = displayName;
        }
    }
}
