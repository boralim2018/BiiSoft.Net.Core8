using Abp.Domain.Entities;
using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ChartOfAccounts
{
    public interface IChartOfAccountManager : IActiveValidateServiceBase<ChartOfAccount, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
  
    }
   
}
