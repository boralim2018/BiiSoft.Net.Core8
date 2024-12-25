using Abp.Domain.Entities;
using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Taxes
{
    public interface ITaxManager : IDefaultActiveValidateServiceBase<Tax, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
 
    }
   
}
