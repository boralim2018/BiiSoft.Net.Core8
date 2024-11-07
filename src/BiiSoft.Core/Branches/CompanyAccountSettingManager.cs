using BiiSoft.ChartOfAccounts;
using BiiSoft.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Branches
{
    public class CompanyAccountSettingManager : BiiSoftValidateServiceBase<CompanyAccountSetting, long>, ICompanyAccountSettingManager
    {
        private readonly IBiiSoftRepository<ChartOfAccount, Guid> _chartOfAccountRepository;
        public CompanyAccountSettingManager(
            IBiiSoftRepository<ChartOfAccount, Guid> chartOfAccountRepository,
            IBiiSoftRepository<CompanyAccountSetting, long> repository) 
        : base(repository)
        {
            _chartOfAccountRepository = chartOfAccountRepository;
        }

        protected override string InstanceName => L("AccountSetting");

        protected override CompanyAccountSetting CreateInstance(CompanyAccountSetting input)
        {
            return CompanyAccountSetting.Create(input.TenantId, input.CreatorUserId, input.DefaultAPAccountId, input.DefaultARAccountId, input.DefaultPurchaseDiscountAccountId, input.DefaultSaleDiscountAccountId, input.DefaultBillPaymentAccountId, input.DefaultReceivePaymentAccountId, input.DefaultInventoryPurchaseAccountId, input.DefaultRetainEarningAccountId, input.DefaultExchangeLossGainAccountId, input.DefaultItemReceiptAccountId, input.DefaultItemIssueAccountId, input.DefaultItemAdjustmentAccountId, input.DefaultItemTransferAccountId, input.DefaultItemProductionAccountId, input.DefaultItemExchangeAccountId, input.DefaultCashTransferAccountId, input.DefaultCashExchangeAccountId);
        }

        protected override void UpdateInstance(CompanyAccountSetting input, CompanyAccountSetting entity)
        {
            entity.Update(input.LastModifierUserId, input.DefaultAPAccountId, input.DefaultARAccountId, input.DefaultPurchaseDiscountAccountId, input.DefaultSaleDiscountAccountId, input.DefaultBillPaymentAccountId, input.DefaultReceivePaymentAccountId, input.DefaultInventoryPurchaseAccountId, input.DefaultRetainEarningAccountId, input.DefaultExchangeLossGainAccountId, input.DefaultItemReceiptAccountId, input.DefaultItemIssueAccountId, input.DefaultItemAdjustmentAccountId, input.DefaultItemTransferAccountId, input.DefaultItemProductionAccountId, input.DefaultItemExchangeAccountId, input.DefaultCashTransferAccountId, input.DefaultCashExchangeAccountId);
        }

        protected override async Task ValidateInputAsync(CompanyAccountSetting input)
        {   
            // Create a list to hold the account IDs to be validated
            var accountIds = new List<Guid>();
            var accountProperties = new (Guid? AccountId, string Label)[]
            {
                (input.DefaultAPAccountId, "APAccount"),
                (input.DefaultARAccountId, "ARAccount"),
                (input.DefaultPurchaseDiscountAccountId, "PurchaseDiscountAccount"),
                (input.DefaultSaleDiscountAccountId, "SaleDiscountAccount"),
                (input.DefaultInventoryPurchaseAccountId, "InventoryPurchaseAccount"),
                (input.DefaultBillPaymentAccountId, "BillPaymentAccount"),
                (input.DefaultReceivePaymentAccountId, "ReceivePaymentAccount"),
                (input.DefaultRetainEarningAccountId, "RetainEarningAccount"),
                (input.DefaultExchangeLossGainAccountId, "ExchangeLossGainAccount"),
                (input.DefaultItemReceiptAccountId, "ItemReceiptAccount"),
                (input.DefaultItemIssueAccountId, "ItemIssueAccount"),
                (input.DefaultItemAdjustmentAccountId, "ItemAdjustmentAccount"),
                (input.DefaultItemTransferAccountId, "ItemTransferAccount"),
                (input.DefaultItemProductionAccountId, "ItemProductionAccount"),
                (input.DefaultItemExchangeAccountId, "ItemExchangeAccount"),
                (input.DefaultCashTransferAccountId, "CashTransferAccount"),
                (input.DefaultCashExchangeAccountId, "CashExchangeAccount")
            };

            // Collect all account IDs that are not null or empty
            foreach (var (accountId, _) in accountProperties)
            {
                if (!accountId.IsNullOrEmpty()) accountIds.Add(accountId.Value);
            }

            // If there are no account IDs to validate, skip the validation process
            if (!accountIds.Any()) return;

            // Fetch all accounts in a single query using HashSet for faster lookup
            var accountList = await _chartOfAccountRepository.GetAll()
                .AsNoTracking()
                .Where(s => accountIds.Contains(s.Id))
                .Select(s => s.Id)
                .ToListAsync();  // Use ToHashSetAsync for better lookup performance

            var accountHash = new HashSet<Guid>(accountList);

            // Validate the accounts by checking if they exist in the fetched account dictionary
            foreach (var (accountId, label) in accountProperties)
            {
                if (accountId.HasValue && !accountHash.Contains(accountId.Value)) InvalidException(L(label));  // Invalid account exception
            }

        }

    }
}
