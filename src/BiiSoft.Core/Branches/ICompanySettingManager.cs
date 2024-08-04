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
        Task<CompanyGeneralSetting> GetAsync(long id);
        Task<CompanyGeneralSetting> GetCompanySettingAsync();
        Task<IdentityResult> CreateAsync(CompanyGeneralSetting @entity);
        Task<IdentityResult> UpdateAsync(CompanyGeneralSetting @entity);
        Task<IdentityResult> RemoveAsync(CompanyGeneralSetting @entity);
    }
   
}
