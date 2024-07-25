using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Partners
{
    public interface IPartnerContactPersonManager : IDomainService
    {
        Task<PartnerContactPerson> GetAsync(Guid id);
        Task<IdentityResult> CreateAsync(PartnerContactPerson @entity);
        Task<IdentityResult> UpdateAsync(PartnerContactPerson @entity);
        Task<IdentityResult> RemoveAsync(PartnerContactPerson @entity);
    }
   
}
