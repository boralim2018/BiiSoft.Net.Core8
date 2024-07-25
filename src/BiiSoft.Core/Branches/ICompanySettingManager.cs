using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Branches
{
    public interface ICompanySettingManager : IDomainService
    {
        Task<CompanySetting> GetAsync(long id);
        Task<CompanySetting> GetCompanySettingAsync();
        Task<IdentityResult> CreateAsync(CompanySetting @entity);
        Task<IdentityResult> UpdateAsync(CompanySetting @entity);
        Task<IdentityResult> RemoveAsync(CompanySetting @entity);
    }
   
}
