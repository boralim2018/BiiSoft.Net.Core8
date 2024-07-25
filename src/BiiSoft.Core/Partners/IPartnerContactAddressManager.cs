using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Partners
{
    public interface IPartnerContactAddressManager : IDomainService
    {
        Task<PartnerContactAddress> GetAsync(Guid id);
        Task<IdentityResult> CreateAsync(PartnerContactAddress @entity);
        Task<IdentityResult> UpdateAsync(PartnerContactAddress @entity);
        Task<IdentityResult> RemoveAsync(PartnerContactAddress @entity);
    }
   
}
