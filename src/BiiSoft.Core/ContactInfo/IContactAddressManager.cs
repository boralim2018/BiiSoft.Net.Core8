using BiiSoft.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiiSoft.ContactInfo
{
    public interface IContactAddressManager : IBiiSoftValidateServiceBase<ContactAddress, Guid>
    {
        Task<IdentityResult> ChangeLocationAsync(ChangeContactAddressLocationInput input);
        Task<IdentityResult> DeleteLocationAsync(IUserEntity<Guid> input);

        Task BulkValidateAsync(List<ContactAddress> input);
        Task<IdentityResult> BulkInsertAsync(IMayHaveTenantBulkInputEntity<ContactAddress> input);
        Task<IdentityResult> BulkUpdateAsync(IBulkInputIntity<ContactAddress> input);
        Task<IdentityResult> BulkDeleteAsync(List<Guid> input);
    }

}
