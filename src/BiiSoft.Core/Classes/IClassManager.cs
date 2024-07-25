using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Classes
{
    public interface IClassManager : IDomainService
    {
        Task<Class> GetAsync(Guid id);
        Task<IdentityResult> CreateAsync(Class @entity);
        Task<IdentityResult> UpdateAsync(Class @entity);
        Task<IdentityResult> RemoveAsync(Class @entity);
    }
   
}
