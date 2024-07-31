using Abp.Domain.Entities;
using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Locations
{
    public interface ISangkatCommuneManager : IDomainService
    {
        Task<SangkatCommune> FindAsync(IEntity<Guid> innput);
        Task<SangkatCommune> GetAsync(IEntity<Guid> innput);
        Task<IdentityResult> InsertAsync(int? tenantId, long userId, SangkatCommune input);
        Task<IdentityResult> UpdateAsync(long userId, SangkatCommune input);
        Task<IdentityResult> DeleteAsync(IEntity<Guid> innput);
        Task<IdentityResult> EnableAsync(long userId, IEntity<Guid> innput);
        Task<IdentityResult> DisableAsync(long userId, IEntity<Guid> innput);
        Task<IdentityResult> ImportAsync(long userId, string fileToken);
    }
   
}
