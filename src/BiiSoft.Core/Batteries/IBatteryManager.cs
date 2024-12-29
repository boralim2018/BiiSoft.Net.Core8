using Abp.Domain.Entities;
using Abp.Domain.Services;
using BiiSoft.Items;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Batteries
{
    public interface IBatteryManager : IDefaultActiveValidateServiceBase<Battery, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
 
    }
   
}
