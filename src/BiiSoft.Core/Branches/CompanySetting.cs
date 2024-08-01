using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using BiiSoft.Currencies;
using BiiSoft.Classes;
using System;
using BiiSoft.ChartOfAccounts;
using System.Text;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using BiiSoft.Enums;

namespace BiiSoft.Branches
{

    [Table("BiiCompanySettings")]
    public class CompanySetting : AuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public Guid? LogoId { get; protected set; }
        public void SetLogo(Guid? logoId) { LogoId = logoId; }

        public bool MultiBranchesEnable { get; protected set; }
        public void EnableMultiBranches(bool enable) { MultiBranchesEnable = enable; }

        public AddressLevel ContactAddressLevel { get; protected set; }

        #region Financing
        public DateTime? BusinessStartDate { get; protected set; }
        public int RoundTotalDigits { get; protected set; }
        public int RoundCostDigts { get; protected set; }
        public bool UseCustomTransactionNo { get; set; }

        public long? CurrencyId { get; protected set; }
        public Currency Currency { get; protected set; }
        public void SetCurrency(long? currencyId) { CurrencyId = currencyId; }
        public bool MultiCurrencyEnable { get; protected set; }
        public void EnableMultiCurrency(bool enable) { MultiCurrencyEnable = enable; }

        public bool UseTradeDiscount { get; protected set; }
        public bool UseCashDiscount { get; protected set; }

        public bool UseClass { get; protected set; }
        public void SetUseClass(bool value) { UseClass = value; }
        public Guid? ClassId { get; protected set; }
        public Class Class { get; protected set; }
        public void SetDefualtClass (Guid? classId) { ClassId = classId; }

        #endregion

        #region Default Setting

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

        public Guid? DefaultReceiptPaymentAccountId { get; protected set; }
        public ChartOfAccount DefaultReceiptPaymentAccount { get; protected set; }

        public Guid? DefaultRetainEarningAccountId { get; protected set; }
        public ChartOfAccount DefaultRetainEarningAccount { get; protected set; }

        public Guid? DefaultExchangeLossGainAccountId { get; protected set; }
        public ChartOfAccount DefaultExchangeLossGainAccount { get; protected set; }

        public Guid? DefaultItemReceipAccountId { get; protected set; }
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

        #endregion


        public static CompanySetting Create(int tenantId, long? userId)
        {
            return new CompanySetting
            {
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
            };
        }

        public static CompanySetting Create(
            int tenantId, 
            long? userId, 
            DateTime? startDate, 
            long? currencyId, 
            bool useCustomTransactionNo, 
            bool useTradeDiscount, 
            bool useCashDiscount, 
            bool multiCurrencyEnable, 
            int roundTotal, 
            int roundCost)
        {
            return new CompanySetting
            {
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                BusinessStartDate = startDate,
                CurrencyId = currencyId,
                UseCustomTransactionNo = useCustomTransactionNo,
                UseTradeDiscount = useTradeDiscount,
                UseCashDiscount = useCashDiscount,
                MultiCurrencyEnable = multiCurrencyEnable,
                RoundTotalDigits = roundTotal,
                RoundCostDigts = roundCost,                
            };
        }

        public static CompanySetting Create(
            int tenantId, 
            long? userId, 
            DateTime? startDate, 
            long? currencyId,
            bool useCustomTransactionNo,
            bool useTradeDiscount, 
            bool useCashDiscount,
            bool multiCurrencyEnable, 
            int roundTotal, 
            int roundCost,
            Guid? apAccountId,
            Guid? arAccountId,
            Guid? purchaseDiscountAccountId,
            Guid? saleDiscountAccountId,           
            Guid? billPaymentAccountId,
            Guid? receiptPaymentAccountId,
            Guid? purchaseAccountId,
            Guid? retainEarningAccountId,
            Guid? exchangeLossGainAccountId,
            Guid? itemReceiptAccountId,
            Guid? itemIssueAccountId,
            Guid? itemAdjustmentAccountId,
            Guid? itemTrasferAccountId,
            Guid? itemProductionAccountId,
            Guid? itemExchangeAccountId,
            Guid? cashTransferAccountId
            )
        {
            return new CompanySetting
            {
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                BusinessStartDate = startDate,
                CurrencyId = currencyId,
                UseCustomTransactionNo = useCustomTransactionNo,
                UseTradeDiscount = useTradeDiscount,
                UseCashDiscount = useCashDiscount,
                MultiCurrencyEnable = multiCurrencyEnable,
                RoundTotalDigits = roundTotal,
                RoundCostDigts = roundCost,
                DefaultARAccountId = arAccountId,
                DefaultAPAccountId = apAccountId,
                DefaultInventoryPurchaseAccountId = purchaseAccountId,
                DefaultPurchaseDiscountAccountId = purchaseDiscountAccountId,
                DefaultSaleDiscountAccountId = saleDiscountAccountId,
                DefaultBillPaymentAccountId = billPaymentAccountId,
                DefaultReceiptPaymentAccountId = receiptPaymentAccountId,
                DefaultRetainEarningAccountId = retainEarningAccountId,
                DefaultExchangeLossGainAccountId = exchangeLossGainAccountId,
                DefaultItemReceipAccountId = itemReceiptAccountId,
                DefaultItemIssueAccountId = itemIssueAccountId,
                DefaultItemAdjustmentAccountId = itemAdjustmentAccountId,
                DefaultItemTransferAccountId = itemTrasferAccountId,
                DefaultItemProductionAccountId = itemProductionAccountId,
                DefaultItemExchangeAccountId = itemExchangeAccountId,
                DefaultCashTransferAccountId = cashTransferAccountId
            };
        }

        public void Update(
            long? userId, 
            DateTime? startDate, 
            long? currencyId, 
            bool useCustomTransactionNo, 
            bool useTradeDiscount, 
            bool useCashDiscount, 
            bool multiCurrencyEnable, 
            int roundTotal, 
            int roundCost)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            BusinessStartDate = startDate;
            CurrencyId = currencyId;
            UseCustomTransactionNo = useCustomTransactionNo;
            UseTradeDiscount = useTradeDiscount;
            UseCashDiscount = useCashDiscount;
            MultiCurrencyEnable = multiCurrencyEnable;
            RoundTotalDigits = roundTotal;
            RoundCostDigts = roundCost;
        }

        public void Update(
            long? userId, 
            DateTime? startDate, 
            long? currencyId, 
            bool useCustomTransactionNo, 
            bool useTradeDiscount, 
            bool useCashDiscount,
            bool multiCurrencyEnable, 
            int roundTotal, 
            int roundCost,
            Guid? apAccountId,
            Guid? arAccountId,
            Guid? purchaseDiscountAccountId,
            Guid? saleDiscountAccountId,
            Guid? billPaymentAccountId,
            Guid? receiptPaymentAccountId,
            Guid? purchaseAccountId,
            Guid? retainEarningAccountId,
            Guid? exchangeLossGainAccountId,
            Guid? itemReceiptAccountId,
            Guid? itemIssueAccountId,
            Guid? itemAdjustmentAccountId,
            Guid? itemTrasferAccountId,
            Guid? itemProductionAccountId,
            Guid? itemExchangeAccountId,
            Guid? cashTransferAccountId
            )
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            BusinessStartDate = startDate;
            CurrencyId = currencyId;
            UseCustomTransactionNo = useCustomTransactionNo;
            UseTradeDiscount = useTradeDiscount;
            UseCashDiscount = useCashDiscount;
            MultiCurrencyEnable = multiCurrencyEnable;
            RoundTotalDigits = roundTotal;
            RoundCostDigts = roundCost;

            DefaultAPAccountId = apAccountId;
            DefaultARAccountId = arAccountId;
            DefaultPurchaseDiscountAccountId = purchaseDiscountAccountId;
            DefaultSaleDiscountAccountId = saleDiscountAccountId;
            DefaultBillPaymentAccountId = billPaymentAccountId;
            DefaultReceiptPaymentAccountId = receiptPaymentAccountId;
            DefaultInventoryPurchaseAccountId = purchaseAccountId;
            DefaultRetainEarningAccountId = retainEarningAccountId;
            DefaultExchangeLossGainAccountId = exchangeLossGainAccountId;
            DefaultItemReceipAccountId = itemReceiptAccountId;
            DefaultItemIssueAccountId = itemIssueAccountId;
            DefaultItemAdjustmentAccountId = itemAdjustmentAccountId;
            DefaultItemTransferAccountId = itemTrasferAccountId;
            DefaultItemProductionAccountId = itemProductionAccountId;
            DefaultItemExchangeAccountId = itemExchangeAccountId;
            DefaultCashTransferAccountId = cashTransferAccountId;
        }

    }
}
