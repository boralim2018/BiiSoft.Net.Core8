using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Partners
{
    public interface IEmployeeManager : IDomainService
    {
        Task<Employee> GetAsync(Guid id);
        Task<IdentityResult> CreateAsync(Employee @entity);
        Task<IdentityResult> UpdateAsync(Employee @entity);
        Task<IdentityResult> RemoveAsync(Employee @entity);
        Task<IdentityResult> EnableAsync(Employee @entity);
        Task<IdentityResult> DisableAsync(Employee @entity);
    }
   
}
