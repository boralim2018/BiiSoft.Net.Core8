using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Branches;
using BiiSoft.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Warehouses
{
    [Table("BiiWarehouseBranchs")]
    public class WarehouseBranch : DefaultEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public Guid WarehouseId { get; private set; }
        public Warehouse Warehouse { get; private set; }
        public Guid BranchId { get; private set; }
        public Branch Branch { get; private set; }

        public static WarehouseBranch Create(int tenantId, long userId, Guid warehouseId, Guid branchId)
        {
            return new WarehouseBranch
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                WarehouseId = warehouseId,
                BranchId = branchId
            };
        }

        public void Update(long userId, Guid warehouseId, Guid branchId)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            WarehouseId = warehouseId;
            BranchId = branchId;
        }
    }
}
