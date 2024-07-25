using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Partners
{
    public interface IPartnerGroupManager : IDomainService
    {
        Task<PartnerGroup> GetAsync(Guid id);
        Task<IdentityResult> CreateAsync(PartnerGroup @entity);
        Task<IdentityResult> UpdateAsync(PartnerGroup @entity);
        Task<IdentityResult> RemoveAsync(PartnerGroup @entity);
    }
   
}
