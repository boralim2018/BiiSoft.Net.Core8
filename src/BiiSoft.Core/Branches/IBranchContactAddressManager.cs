using BiiSoft.ContactInfo;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiiSoft.Branches
{
    public interface IBranchContactAddressManager : IContactAddressBaseManager<BranchContactAddress, Guid>
    {
        Task<IdentityResult> SetAsDefaultAsync(long userId, Guid id);
        Task<IdentityResult> BulkSyncAsync(int? tenantId, long userId, Guid branchId, List<BranchContactAddress> input);
    }
}
