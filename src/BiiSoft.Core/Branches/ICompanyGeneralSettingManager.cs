using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Branches
{
    public interface ICompanyGeneralSettingManager : IBiiSoftValidateServiceBase<CompanyGeneralSetting, long>
    {
       
    }
   
}
