using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using BiiSoft.ChartOfAccounts;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Branches
{

    [Table("BiiCompanyAccountSettings")]
    public class CompanyAccountSetting : AuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public Guid? DefaultAPAccountId { get; protected set; }
        public ChartOfAccount DefaultAPAccount { get; protected set; }

        public Guid? DefaultARAccountId { get; protected set; }
        public ChartOfAccount DefaultARAccount { get; protected set; }

        public Guid? DefaultPurchaseDiscountAccountId { get; protected set; }
        public ChartOfAccount DefaultPurchaseDiscountAccount { get; protected set; }

        public Guid? DefaultSaleDiscountAccountId { get; protected set; }
        public ChartOfAccount DefaultSaleDiscountAccount { get; protected set; }

        public Guid? DefaultInventoryPurchaseAccountId { get; protected set; }
        public ChartOfAccount DefaultInventoryPurchaseAccount { get; protected set; }
        
        public Guid? DefaultBillPaymentAccountId { get; protected set; }
        public ChartOfAccount DefaultBillPaymentAccount { get; protected set; }

        public Guid? DefaultReceivePaymentAccountId { get; protected set; }
        public ChartOfAccount DefaultReceivePaymentAccount { get; protected set; }

        public Guid? DefaultRetainEarningAccountId { get; protected set; }
        public ChartOfAccount DefaultRetainEarningAccount { get; protected set; }

        public Guid? DefaultExchangeLossGainAccountId { get; protected set; }
        public ChartOfAccount DefaultExchangeLossGainAccount { get; protected set; }

        public Guid? DefaultItemReceiptAccountId { get; protected set; }
        public ChartOfAccount DefaultItemReceiptAccount { get; protected set; }

        public Guid? DefaultItemIssueAccountId { get; protected set; }
        public ChartOfAccount DefaultItemIssueAccount { get; protected set; }

        public Guid? DefaultItemAdjustmentAccountId { get; protected set; }
        public ChartOfAccount DefaultItemAdjustmentAccount { get; protected set; }

        public Guid? DefaultItemTransferAccountId { get; protected set; }
        public ChartOfAccount DefaultItemTransferAccount { get; protected set; }

        public Guid? DefaultItemProductionAccountId { get; protected set; }
        public ChartOfAccount DefaultItemProductionAccount { get; protected set; }

        public Guid? DefaultItemExchangeAccountId { get; protected set; }
        public ChartOfAccount DefaultItemExchangeAccount { get; protected set; }

        public Guid? DefaultCashTransferAccountId { get; protected set; }
        public ChartOfAccount DefaultCashTransferAccount { get; protected set; }

        public Guid? DefaultCashExchangeAccountId { get; protected set; }
        public ChartOfAccount DefaultCashExchangeAccount { get; protected set; }


        public static CompanyAccountSetting Create(
            int tenantId, 
            long? userId, 
            Guid? apAccountId,
            Guid? arAccountId,
            Guid? purchaseDiscountAccountId,
            Guid? saleDiscountAccountId,           
            Guid? billPaymentAccountId,
            Guid? receivePaymentAccountId,
            Guid? purchaseAccountId,
            Guid? retainEarningAccountId,
            Guid? exchangeLossGainAccountId,
            Guid? itemReceiptAccountId,
            Guid? itemIssueAccountId,
            Guid? itemAdjustmentAccountId,
            Guid? itemTrasferAccountId,
            Guid? itemProductionAccountId,
            Guid? itemExchangeAccountId,
            Guid? cashTransferAccountId,
            Guid? cashExchangeAccountId
            )
        {
            return new CompanyAccountSetting
            {
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                DefaultARAccountId = arAccountId,
                DefaultAPAccountId = apAccountId,
                DefaultInventoryPurchaseAccountId = purchaseAccountId,
                DefaultPurchaseDiscountAccountId = purchaseDiscountAccountId,
                DefaultSaleDiscountAccountId = saleDiscountAccountId,
                DefaultBillPaymentAccountId = billPaymentAccountId,
                DefaultReceivePaymentAccountId = receivePaymentAccountId,
                DefaultRetainEarningAccountId = retainEarningAccountId,
                DefaultExchangeLossGainAccountId = exchangeLossGainAccountId,
                DefaultItemReceiptAccountId = itemReceiptAccountId,
                DefaultItemIssueAccountId = itemIssueAccountId,
                DefaultItemAdjustmentAccountId = itemAdjustmentAccountId,
                DefaultItemTransferAccountId = itemTrasferAccountId,
                DefaultItemProductionAccountId = itemProductionAccountId,
                DefaultItemExchangeAccountId = itemExchangeAccountId,
                DefaultCashTransferAccountId = cashTransferAccountId,
                DefaultCashExchangeAccountId = cashExchangeAccountId
            };
        }

        public void Update(
            long? userId,
            Guid? apAccountId,
            Guid? arAccountId,
            Guid? purchaseDiscountAccountId,
            Guid? saleDiscountAccountId,
            Guid? billPaymentAccountId,
            Guid? receivePaymentAccountId,
            Guid? purchaseAccountId,
            Guid? retainEarningAccountId,
            Guid? exchangeLossGainAccountId,
            Guid? itemReceiptAccountId,
            Guid? itemIssueAccountId,
            Guid? itemAdjustmentAccountId,
            Guid? itemTrasferAccountId,
            Guid? itemProductionAccountId,
            Guid? itemExchangeAccountId,
            Guid? cashTransferAccountId,
            Guid? cashExchangeAccountId
            )
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            DefaultAPAccountId = apAccountId;
            DefaultARAccountId = arAccountId;
            DefaultPurchaseDiscountAccountId = purchaseDiscountAccountId;
            DefaultSaleDiscountAccountId = saleDiscountAccountId;
            DefaultBillPaymentAccountId = billPaymentAccountId;
            DefaultReceivePaymentAccountId = receivePaymentAccountId;
            DefaultInventoryPurchaseAccountId = purchaseAccountId;
            DefaultRetainEarningAccountId = retainEarningAccountId;
            DefaultExchangeLossGainAccountId = exchangeLossGainAccountId;
            DefaultItemReceiptAccountId = itemReceiptAccountId;
            DefaultItemIssueAccountId = itemIssueAccountId;
            DefaultItemAdjustmentAccountId = itemAdjustmentAccountId;
            DefaultItemTransferAccountId = itemTrasferAccountId;
            DefaultItemProductionAccountId = itemProductionAccountId;
            DefaultItemExchangeAccountId = itemExchangeAccountId;
            DefaultCashTransferAccountId = cashTransferAccountId;
            DefaultCashExchangeAccountId = cashExchangeAccountId;
        }

    }
}
