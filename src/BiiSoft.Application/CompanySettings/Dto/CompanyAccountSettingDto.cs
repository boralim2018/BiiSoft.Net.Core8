using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using BiiSoft.ChartOfAccounts;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Branches
{

    public class CompanyAccountSettingDto 
    {
        public long? Id { get; set; }

        public bool CustomAccountCodeEnable { get; set; }

        public Guid? DefaultAPAccountId { get; set; }
        public string DefaultAPAccountName { get; set; }

        public Guid? DefaultARAccountId { get; set; }
        public string DefaultARAccountName { get; set; }

        public Guid? DefaultPurchaseDiscountAccountId { get; set; }
        public string DefaultPurchaseDiscountAccountName { get; set; }

        public Guid? DefaultSaleDiscountAccountId { get; set; }
        public string DefaultSaleDiscountAccountName { get; set; }

        public Guid? DefaultInventoryPurchaseAccountId { get; set; }
        public string DefaultInventoryPurchaseAccountName { get; set; }
        
        public Guid? DefaultBillPaymentAccountId { get; set; }
        public string DefaultBillPaymentAccountName { get; set; }

        public Guid? DefaultReceivePaymentAccountId { get; set; }
        public string DefaultReceivePaymentAccountName { get; set; }

        public Guid? DefaultRetainEarningAccountId { get; set; }
        public string DefaultRetainEarningAccountName { get; set; }

        public Guid? DefaultExchangeLossGainAccountId { get; set; }
        public string DefaultExchangeLossGainAccountName { get; set; }

        public Guid? DefaultItemReceiptAccountId { get; set; }
        public string DefaultItemReceiptAccountName { get; set; }

        public Guid? DefaultItemIssueAccountId { get; set; }
        public string DefaultItemIssueAccountName { get; set; }

        public Guid? DefaultItemAdjustmentAccountId { get; set; }
        public string DefaultItemAdjustmentAccountName { get; set; }

        public Guid? DefaultItemTransferAccountId { get; set; }
        public string DefaultItemTransferAccountName { get; set; }

        public Guid? DefaultItemProductionAccountId { get; set; }
        public string DefaultItemProductionAccountName { get; set; }

        public Guid? DefaultItemExchangeAccountId { get; set; }
        public string DefaultItemExchangeAccountName { get; set; }
        public Guid? DefaultCashTransferAccountId { get; set; }
        public string DefaultCashTransferAccountName { get; set; }
        public Guid? DefaultCashExchangeAccountId { get; set; }
        public string DefaultCashExchangeAccountName { get; set; }

    }
}
