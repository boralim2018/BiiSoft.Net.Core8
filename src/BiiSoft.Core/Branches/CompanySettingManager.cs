using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Branches
{
    public class CompanySettingManager : ICompanySettingManager
    {
        private readonly IRepository<CompanySetting, long> _repository;
        public CompanySettingManager(IRepository<CompanySetting, long> repository)
        {
            _repository = repository;
        }

        public async Task<IdentityResult> CreateAsync(CompanySetting @entity)
        {
            await _repository.InsertAsync(@entity);
            return IdentityResult.Success;
        }

        public async Task<CompanySetting> GetAsync(long id)
        {
            return await _repository.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<CompanySetting> GetCompanySettingAsync()
        {
            return await _repository.GetAll()
                .Include(s => s.Currency)
                .Include(s => s.DefaultAPAccount)
                .Include(s => s.DefaultARAccount)
                .Include(s => s.DefaultInventoryPurchaseAccount)
                .Include(s => s.DefaultPurchaseDiscountAccount)
                .Include(s => s.DefaultSaleDiscountAccount)
                .Include(s => s.DefaultBillPaymentAccount)
                .Include(s => s.DefaultReceiptPaymentAccount)
                .Include(s => s.DefaultExchangeLossGainAccount)   
                .Include(s => s.DefaultItemReceiptAccount)   
                .Include(s => s.DefaultItemIssueAccount)   
                .Include(s => s.DefaultItemAdjustmentAccount)   
                .Include(s => s.DefaultItemTransferAccount)   
                .Include(s => s.DefaultItemProductionAccount)   
                .Include(s => s.DefaultItemExchangeAccount)   
                .Include(s => s.DefaultCashTransferAccount)   
                .Include(s => s.DefaultRetainEarningAccount.AccountType)   
                .Include(s => s.Class)
                .FirstOrDefaultAsync();
        }

        public async Task<IdentityResult> RemoveAsync(CompanySetting @entity)
        {
            await _repository.DeleteAsync(@entity);
            return IdentityResult.Success;           
        }

        public async Task<IdentityResult> UpdateAsync(CompanySetting @entity)
        {
            await _repository.UpdateAsync(@entity);
            return IdentityResult.Success;
        }
    }
}
