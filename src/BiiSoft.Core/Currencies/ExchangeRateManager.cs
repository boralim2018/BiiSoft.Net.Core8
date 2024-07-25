using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Currencies
{
    public class ExchangeRateManager : IExchangeRateManager
    {
        private readonly IRepository<ExchangeRate, Guid> _repository;
        public ExchangeRateManager(IRepository<ExchangeRate, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<IdentityResult> CreateAsync(ExchangeRate @entity)
        {
            await _repository.InsertAsync(@entity);
            return IdentityResult.Success;
        }

        public async Task<ExchangeRate> GetAsync(Guid id)
        {
            return await _repository.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IdentityResult> RemoveAsync(ExchangeRate @entity)
        {
            await _repository.DeleteAsync(@entity);
            return IdentityResult.Success;           
        }

        public async Task<IdentityResult> UpdateAsync(ExchangeRate @entity)
        {
            await _repository.UpdateAsync(@entity);
            return IdentityResult.Success;
        }
    }
}
