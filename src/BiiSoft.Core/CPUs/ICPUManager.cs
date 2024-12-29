using Abp.Domain.Entities;
using Abp.Domain.Services;
using BiiSoft.Items;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.CPUs
{
    public interface ICPUManager : IDefaultActiveValidateServiceBase<CPU, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
 
    }
   
}
