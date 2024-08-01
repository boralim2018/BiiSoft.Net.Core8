using Abp.Domain.Entities;
using Abp.Domain.Services;
using BiiSoft.ContactInfo;
using BiiSoft.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Branches
{
    public interface IBranchManager : IDefaultActiveValidateServiceBase<Branch, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
        

    }
   
}
