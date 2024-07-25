using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Timing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BiiSoft.Extensions;
using BiiSoft.Entities;
using BiiSoft.ChartOfAccounts;
using BiiSoft.Taxes;

namespace BiiSoft.Items
{
    [Table("BiiItemGroups")]
    public class ItemGroup : DefaultNameActiveEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public Guid? PurchaseTaxId { get; private set; }
        public Tax PurchaseTax { get; private set; }
        public void SetPurchaseTax(Guid? taxId) => PurchaseTaxId = taxId;
        public Guid? SaleTaxId { get; private set; }
        public Tax SaleTax { get; private set; }
        public void SetSaleTax(Guid? taxId) => SaleTaxId = taxId;

        public Guid? PurchaseAccountId { get; private set; }
        public ChartOfAccount PurchaseAccount { get; private set; }
        public Guid? SaleAccountId { get; private set; }
        public ChartOfAccount SaleAccount { get; private set; }

        public Guid? InventoryAccountId { get; private set; }
        public ChartOfAccount InventoryAccount { get; private set; }

        public static ItemGroup Create(
            int tenantId, 
            long userId, 
            string name, 
            string displayName, 
            Guid? purchaseTaxId, 
            Guid? saleTaxId, 
            Guid? purchaseAccountId, 
            Guid? saleAccountId, 
            Guid? inventoryAccountId)
        {
            return new ItemGroup
            {
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                PurchaseTaxId = purchaseTaxId,
                SaleTaxId = saleTaxId,
                PurchaseAccountId = purchaseAccountId,
                SaleAccountId = saleAccountId,
                InventoryAccountId = inventoryAccountId,
                IsActive = true
            };
        }

        public void Update(
            long userId, 
            string name,
            string displayName,
            Guid? purchaseTaxId,
            Guid? saleTaxId,
            Guid? purchaseAccountId,
            Guid? saleAccountId,
            Guid? inventoryAccountId)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Name = name;
            DisplayName = displayName;
            PurchaseTaxId = purchaseTaxId;
            SaleAccountId = saleTaxId;
            PurchaseAccountId = purchaseAccountId;
            SaleAccountId = saleAccountId;
            InventoryAccountId = inventoryAccountId;
        }

    }
}
