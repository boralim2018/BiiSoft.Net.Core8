using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Enums
{
    public enum JournalType
    {
        GeneralJournal = 1,

        CashIn = 2,
        CashOut = 3,
        CashTransfer = 4,
        CashExchange = 5,

        Bill = 6,
        PurchaseReturn = 7,
        DebitNote = 8,
        BillPayment = 9,
        VendorRefund = 10,

        Invoice = 11,
        SaleReturn = 12,
        CreditNote = 13,
        ReceivePayment = 14,
        CustomerRefund = 15,

        ItemReceipt = 16,
        ItemIssue = 17,
        ItemTransfer = 18,
        ItemExchange = 19,
        Production = 20,
        PhysicalCount = 21
    }
}
