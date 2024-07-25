using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Partners
{
    public interface IPartnerManager : IDomainService
    {
        Task<Partner> GetAsync(Guid id);
        Task<IdentityResult> CreateAsync(Partner @entity);
        Task<IdentityResult> UpdateAsync(Partner @entity);
        Task<IdentityResult> RemoveAsync(Partner @entity);
        Task<IdentityResult> EnableAsync(Partner @entity);
        Task<IdentityResult> DisableAsync(Partner @entity);
    }
   
}
