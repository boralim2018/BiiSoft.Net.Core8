using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Branches
{
    public interface IBranchManager : IDomainService
    {
        Task<Branch> GetCompanyInfoAsync();
        Task<Branch> GetAsync(Guid id, bool readOnly = true);
        Task<IdentityResult> InsertAsync(int? tenantId, long userId, Branch input, List<BranchContactAddress> addresses);
        Task<IdentityResult> UpdateAsync(long userId, Branch input, List<BranchContactAddress> addresses);
        Task<IdentityResult> DeleteAsync(Guid id);
        Task<IdentityResult> EnableAsync(long userId, Guid id);
        Task<IdentityResult> DisableAsync(long userId, Guid id);
        Task<IdentityResult> SetAsDefaultAsync(long userId, Guid id);
        Task<IdentityResult> ImportAsync(int? tenantId, long userId, string fileToken);

    }
   
}
