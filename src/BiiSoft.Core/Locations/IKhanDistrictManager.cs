using Abp.Domain.Entities;
using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Locations
{
    public interface IKhanDistrictManager : IDomainService
    {
        Task<KhanDistrict> FindAsync(IEntity<Guid> innput);
        Task<KhanDistrict> GetAsync(IEntity<Guid> innput);
        Task<IdentityResult> InsertAsync(long userId, KhanDistrict input);
        Task<IdentityResult> UpdateAsync(long userId, KhanDistrict input);
        Task<IdentityResult> DeleteAsync(IEntity<Guid> innput);
        Task<IdentityResult> EnableAsync(long userId, IEntity<Guid> innput);
        Task<IdentityResult> DisableAsync(long userId, IEntity<Guid> innput);
        Task<IdentityResult> ImportAsync(long userId, string fileToken);
    }
   
}
