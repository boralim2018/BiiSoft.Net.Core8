using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Currencies
{
    public interface IMultiCurrencyManager : IDomainService
    {
        Task<MultiCurrency> GetAsync(Guid id);
        Task<IdentityResult> CreateAsync(MultiCurrency @entity);
        Task<IdentityResult> UpdateAsync(MultiCurrency @entity);
        Task<IdentityResult> RemoveAsync(MultiCurrency @entity);
    }
   
}
