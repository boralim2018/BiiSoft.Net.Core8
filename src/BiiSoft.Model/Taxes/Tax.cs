using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BiiSoft.ChartOfAccounts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using BiiSoft.Entities;

namespace BiiSoft.Taxes
{
    [Table("BiiTaxes")]
    public class Tax : CanModifyDefaultNameActiveEntity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        
        [Precision(18, 2)]
        public decimal Rate { get; private set; } 

        public Guid? PurchaseAccountId { get; private set; }
        public ChartOfAccount PurchaseAccount { get; private set; }
        public Guid? SaleAccountId { get; private set; }
        public ChartOfAccount SaleAccount { get; private set; }

        public static Tax Create(int? tenantId, long userId, string name, string displayName, decimal rate, Guid? purchaseAccountId, Guid? saleAccountId)
        {
            return new Tax
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Rate = rate,
                PurchaseAccountId = purchaseAccountId,
                SaleAccountId = saleAccountId,
                IsActive = true
            };
        }

        public void Update(long userId, string name, string displayName, decimal rate, Guid? purchaseAccountId, Guid? saleAccountId)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Name = name;
            DisplayName = displayName;
            Rate = rate;
            PurchaseAccountId = purchaseAccountId;
            SaleAccountId = saleAccountId;
        }
    }
}
