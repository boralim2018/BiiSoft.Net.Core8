using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using BiiSoft.ChartOfAccounts;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.CompanySettings.Dto
{

    public class CreateUpdateCompanyAccountSettingInputDto
    {
        public long? Id { get; set; }

        public Guid? DefaultAPAccountId { get; set; }
        public Guid? DefaultARAccountId { get; set; }
        public Guid? DefaultPurchaseDiscountAccountId { get; set; }
        public Guid? DefaultSaleDiscountAccountId { get; set; }
        public Guid? DefaultInventoryPurchaseAccountId { get; set; }
        public Guid? DefaultBillPaymentAccountId { get; set; }
        public Guid? DefaultReceivePaymentAccountId { get; set; }
        public Guid? DefaultRetainEarningAccountId { get; set; }
        public Guid? DefaultExchangeLossGainAccountId { get; set; }
        public Guid? DefaultItemReceiptAccountId { get; set; }
        public Guid? DefaultItemIssueAccountId { get; set; }
        public Guid? DefaultItemAdjustmentAccountId { get; set; }
        public Guid? DefaultItemTransferAccountId { get; set; }
        public Guid? DefaultItemProductionAccountId { get; set; }
        public Guid? DefaultItemExchangeAccountId { get; set; }
        public Guid? DefaultCashTransferAccountId { get; set; }
        public Guid? DefaultCashExchangeAccountId { get; set; }

    }
}
