using BiiSoft.ChartOfAccounts;
using BiiSoft.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
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
            return CompanyAccountSetting.Create(input.TenantId, input.CreatorUserId, input.DefaultAPAccountId, input.DefaultARAccountId, input.DefaultPurchaseDiscountAccountId, input.DefaultSaleDiscountAccountId, input.DefaultBillPaymentAccountId, input.DefaultReceiptPaymentAccountId, input.DefaultInventoryPurchaseAccountId, input.DefaultRetainEarningAccountId, input.DefaultExchangeLossGainAccountId, input.DefaultItemReceiptAccountId, input.DefaultItemIssueAccountId, input.DefaultItemAdjustmentAccountId, input.DefaultItemTransferAccountId, input.DefaultItemProductionAccountId, input.DefaultItemExchangeAccountId, input.DefaultCashTransferAccountId, input.DefaultCashExchangeAccountId);
        }

        protected override void UpdateInstance(CompanyAccountSetting input, CompanyAccountSetting entity)
        {
            entity.Update(input.LastModifierUserId, input.DefaultAPAccountId, input.DefaultARAccountId, input.DefaultPurchaseDiscountAccountId, input.DefaultSaleDiscountAccountId, input.DefaultBillPaymentAccountId, input.DefaultReceiptPaymentAccountId, input.DefaultInventoryPurchaseAccountId, input.DefaultRetainEarningAccountId, input.DefaultExchangeLossGainAccountId, input.DefaultItemReceiptAccountId, input.DefaultItemIssueAccountId, input.DefaultItemAdjustmentAccountId, input.DefaultItemTransferAccountId, input.DefaultItemProductionAccountId, input.DefaultItemExchangeAccountId, input.DefaultCashTransferAccountId, input.DefaultCashExchangeAccountId);
        }

        protected override async Task ValidateInputAsync(CompanyAccountSetting input)
        {
            var accountIdDic = await _chartOfAccountRepository.GetAll().AsNoTracking().Select(s => s.Id).ToListAsync();

            if (!input.DefaultAPAccountId.IsNullOrEmpty())
            {

            }

            if (!input.DefaultARAccountId.IsNullOrEmpty()) ValidateSelect(input.DefaultARAccountId, L("ARAccount"));
            if (!input.DefaultPurchaseDiscountAccountId.IsNullOrEmpty()) ValidateSelect(input.DefaultPurchaseDiscountAccountId, L("PurchaseDiscountAccount"));
            if (!input.DefaultSaleDiscountAccountId.IsNullOrEmpty()) ValidateSelect(input.DefaultSaleDiscountAccountId, L("SaleDiscountAccount"));
            if (!input.DefaultInventoryPurchaseAccountId.IsNullOrEmpty()) ValidateSelect(input.DefaultInventoryPurchaseAccountId, L("InventoryPurchaseAccount"));
            if (!input.DefaultBillPaymentAccountId.IsNullOrEmpty()) ValidateSelect(input.DefaultBillPaymentAccountId, L("BillPaymentAccount"));
            if (!input.DefaultReceiptPaymentAccountId.IsNullOrEmpty()) ValidateSelect(input.DefaultReceiptPaymentAccountId, L("ReceiptPaymentAccount"));
            if (!input.DefaultAPAccountId.IsNullOrEmpty()) ValidateSelect(input.DefaultAPAccountId, L("APAccount"));
            if (!input.DefaultAPAccountId.IsNullOrEmpty()) ValidateSelect(input.DefaultAPAccountId, L("APAccount"));
            if (!input.DefaultAPAccountId.IsNullOrEmpty()) ValidateSelect(input.DefaultAPAccountId, L("APAccount"));
            if (!input.DefaultAPAccountId.IsNullOrEmpty()) ValidateSelect(input.DefaultAPAccountId, L("APAccount"));
            if (!input.DefaultAPAccountId.IsNullOrEmpty()) ValidateSelect(input.DefaultAPAccountId, L("APAccount"));
            if (!input.DefaultAPAccountId.IsNullOrEmpty()) ValidateSelect(input.DefaultAPAccountId, L("APAccount"));
            if (!input.DefaultAPAccountId.IsNullOrEmpty()) ValidateSelect(input.DefaultAPAccountId, L("APAccount"));
        }

    }
}
