using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Locations
{
    public interface ICountryManager : IDomainService
    {
        Task<Country> GetAsync(Guid id, bool readOnly = true);
        Task<IdentityResult> InsertAsync(int? tenantId, long userId, Country input);
        Task<IdentityResult> UpdateAsync(long userId, Country input);
        Task<IdentityResult> DeleteAsync(Guid id);
        Task<IdentityResult> EnableAsync(long userId, Guid id);
        Task<IdentityResult> DisableAsync(long userId, Guid id);
        Task<IdentityResult> ImportAsync(long userId, string fileToken);
    }
   
}
