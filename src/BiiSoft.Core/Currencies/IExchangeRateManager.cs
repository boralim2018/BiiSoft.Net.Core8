using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Currencies
{
    public interface IExchangeRateManager : IDomainService
    {
        Task<ExchangeRate> GetAsync(Guid id);
        Task<IdentityResult> CreateAsync(ExchangeRate @entity);
        Task<IdentityResult> UpdateAsync(ExchangeRate @entity);
        Task<IdentityResult> RemoveAsync(ExchangeRate @entity);
    }
   
}
