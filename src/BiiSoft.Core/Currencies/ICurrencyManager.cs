using Abp.Domain.Services;
using BiiSoft.Locations;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Currencies
{
    public interface ICurrencyManager : IDomainService
    {
        Task<Currency> GetAsync(long id, bool readOnly = true);
        Task<IdentityResult> InsertAsync(int? tenantId, long userId, Currency input);
        Task<IdentityResult> UpdateAsync(long userId, Currency input);
        Task<IdentityResult> DeleteAsync(long id);
        Task<IdentityResult> EnableAsync(long userId, long id);
        Task<IdentityResult> DisableAsync(long userId, long id);      
        Task<IdentityResult> ImportAsync(long userId, string fileTokken);
        Task<IdentityResult> SetAsDefaultAsync(long userId, long id);
    }
   
}
