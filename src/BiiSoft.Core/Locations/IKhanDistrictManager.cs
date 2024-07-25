using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Locations
{
    public interface IKhanDistrictManager : IDomainService
    {
        Task<KhanDistrict> GetAsync(Guid id, bool readOnly = true);
        Task<IdentityResult> InsertAsync(int? tenantId, long userId, KhanDistrict input);
        Task<IdentityResult> UpdateAsync(long userId, KhanDistrict input);
        Task<IdentityResult> DeleteAsync(Guid id);
        Task<IdentityResult> EnableAsync(long userId, Guid id);
        Task<IdentityResult> DisableAsync(long userId, Guid id);
        Task<IdentityResult> ImportAsync(long userId, string fileToken);
    }
   
}
