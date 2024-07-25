using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Partners
{
    public interface IEmployeeContactAddressManager : IDomainService
    {
        Task<EmployeeContactAddress> GetAsync(Guid id);
        Task<IdentityResult> CreateAsync(EmployeeContactAddress @entity);
        Task<IdentityResult> UpdateAsync(EmployeeContactAddress @entity);
        Task<IdentityResult> RemoveAsync(EmployeeContactAddress @entity);
    }
   
}
