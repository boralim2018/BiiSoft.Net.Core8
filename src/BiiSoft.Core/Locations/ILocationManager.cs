using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Locations
{
    public interface ILocationManager : IDomainService
    {
        Task<Location> GetAsync(Guid id, bool readOnly = true);
        Task<IdentityResult> InsertAsync(int? tenantId, long userId, Location input);
        Task<IdentityResult> UpdateAsync(long userId, Location input);
        Task<IdentityResult> DeleteAsync(Guid id);
        Task<IdentityResult> EnableAsync(long userId, Guid id);
        Task<IdentityResult> DisableAsync(long userId, Guid id);
        Task<IdentityResult> ImportAsync(int? tenantId, long userId, string fileToken);
    }
   
}
