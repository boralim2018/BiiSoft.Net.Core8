using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Currencies
{
    public class MultiCurrencyManager : IMultiCurrencyManager
    {
        private readonly IRepository<MultiCurrency, Guid> _repository;
        public MultiCurrencyManager(IRepository<MultiCurrency, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<IdentityResult> CreateAsync(MultiCurrency @entity)
        {
            await _repository.InsertAsync(@entity);
            return IdentityResult.Success;
        }

        public async Task<MultiCurrency> GetAsync(Guid id)
        {
            return await _repository.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IdentityResult> RemoveAsync(MultiCurrency @entity)
        {
            await _repository.DeleteAsync(@entity);
            return IdentityResult.Success;           
        }

        public async Task<IdentityResult> UpdateAsync(MultiCurrency @entity)
        {
            await _repository.UpdateAsync(@entity);
            return IdentityResult.Success;
        }
    }
}
