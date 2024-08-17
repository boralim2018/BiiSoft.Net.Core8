using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Enums
{
    public enum InventoryType
    {
        ItemReceiptPurchase = 1,
        ItemReceiptTransfer = 2,
        ItemReceiptProduction = 3,
        ItemReceiptExchangeItem = 4,
        ItemReceiptSaleReturn = 5,
        ItemReceiptBorrowReturn = 6,
        ItemReceiptAdjustment = 7,
        ItemReceiptOther = 8,

        ItemIssueSale = 9,
        ItemIssueTransfer = 10,
        ItemIssueProduction = 11,
        ItemIssueExchangeItem = 12,
        ItemIssuePurchaseReturn = 13,
        ItemIssueBorrow = 14,
        ItemIssueAdjustment = 15,
        ItemIssueOther = 16,
    }
}
