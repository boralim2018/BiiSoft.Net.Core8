using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiiSoft.Enums;

namespace BiiSoft.Extensions
{
    public static class JournalTypeExtensions
    {

        private static readonly Dictionary<JournalType, string> Prefixes = new()
        {
            { JournalType.GeneralJournal, "GRNY-" },
            { JournalType.CashIn, "CINY-" },
            { JournalType.CashOut, "COUY-" },
            { JournalType.CashTransfer, "CTRY-" },
            { JournalType.CashExchange, "CEXY-" },
            { JournalType.Bill, "BILY-" },
            { JournalType.PurchaseReturn, "PREY-" },
            { JournalType.DebitNote, "DBNY-" },
            { JournalType.BillPayment, "BPAY-" },
            { JournalType.VendorRefund, "VREY-" },
            { JournalType.Invoice, "INVY-" },
            { JournalType.SaleReturn, "SREY-" },
            { JournalType.CreditNote, "CDNY-" },
            { JournalType.ReceivePayment, "RPAY-" },
            { JournalType.CustomerRefund, "CREY-" },
            { JournalType.ItemReceipt, "IRCY-" },
            { JournalType.ItemIssue, "IISY-" },
            { JournalType.ItemTransfer, "ITRY-" },
            { JournalType.ItemExchange, "IEXY-" },
            { JournalType.Production, "PROY-" },
            { JournalType.PhysicalCount, "PHCY-" }
        };

        public static string GetPrefix(this JournalType type)
        {
            return Prefixes.TryGetValue(type, out var prefix) ? prefix : "GRNY-";
        }
    }
}
