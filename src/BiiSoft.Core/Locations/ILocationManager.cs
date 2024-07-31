using Abp.Domain.Entities;
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
        Task<Location> FindAsync(IEntity<Guid> innput);
        Task<Location> GetAsync(IEntity<Guid> innput);
        Task<IdentityResult> InsertAsync(long userId, Location input);
        Task<IdentityResult> UpdateAsync(long userId, Location input);
        Task<IdentityResult> DeleteAsync(IEntity<Guid> innput);
        Task<IdentityResult> EnableAsync(long userId, IEntity<Guid> innput);
        Task<IdentityResult> DisableAsync(long userId, IEntity<Guid> innput);
        Task<IdentityResult> ImportAsync(int? tenantId, long userId, string fileToken);
    }
   
}
