using Abp.Domain.Services;
using BiiSoft.Branches;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ContactInfo
{
    public interface IContactAddressManager : IDomainService
    {
        Task<ContactAddress> GetAsync(Guid id, bool readOnly = true);
        Task<IdentityResult> InsertAsync(int? tenantId, long userId, ContactAddress input);
        Task<IdentityResult> UpdateAsync(long userId, ContactAddress input);
        Task<IdentityResult> DeleteAsync(Guid id);
        Task<IdentityResult> ChangeLocationAsync(long userId, Guid? locationId, Guid id);

        Task BulkValidateAsync(List<ContactAddress> input);
        Task<IdentityResult> BulkInsertAsync(int? tenantId, long userId, List<ContactAddress> input);
        Task<IdentityResult> BulkUpdateAsync(long userId, List<ContactAddress> input);
        Task<IdentityResult> BulkDeleteAsync(List<Guid> input);
    }

}
