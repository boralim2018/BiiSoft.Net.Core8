using Abp.Domain.Entities;
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
        Task<Country> FindAsync(IEntity<Guid> input);
        Task<Country> GetAsync(IEntity<Guid> input);
        Task<IdentityResult> InsertAsync(long userId, Country input);
        Task<IdentityResult> UpdateAsync(long userId, Country input);
        Task<IdentityResult> DeleteAsync(IEntity<Guid> input);
        Task<IdentityResult> EnableAsync(long userId, IEntity<Guid> input);
        Task<IdentityResult> DisableAsync(long userId, IEntity<Guid> input);
        Task<IdentityResult> ImportAsync(long userId, string fileToken);
    }
   
}
