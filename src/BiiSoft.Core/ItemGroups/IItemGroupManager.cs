using Abp.Domain.Entities;
using Abp.Domain.Services;
using BiiSoft.Items;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ItemGroups
{
    public interface IItemGroupManager : IDefaultActiveValidateServiceBase<ItemGroup, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
 
    }
   
}
