using Abp.Domain.Entities;
using Abp.Domain.Services;
using BiiSoft.Items;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Units
{
    public interface IUnitManager : IDefaultActiveValidateServiceBase<Unit, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
 
    }
   
}
