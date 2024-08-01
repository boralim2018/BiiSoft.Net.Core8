using Abp.Domain.Entities;
using Abp.Domain.Services;
using BiiSoft.Branches;
using BiiSoft.Locations;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ContactInfo
{
    public interface IContactAddressManager : IBiiSoftValidateServiceBase<ContactAddress, Guid>
    {
        Task<IdentityResult> ChangeLocationAsync(long userId, IEntity<Guid?> location, IEntity<Guid> input);

        Task BulkValidateAsync(List<ContactAddress> input);
        Task<IdentityResult> BulkInsertAsync(int? tenantId, long userId, List<ContactAddress> input);
        Task<IdentityResult> BulkUpdateAsync(long userId, List<ContactAddress> input);
        Task<IdentityResult> BulkDeleteAsync(List<Guid> input);
    }

}
