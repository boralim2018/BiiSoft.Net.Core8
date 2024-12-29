using Abp.Domain.Entities;
using Abp.Domain.Services;
using BiiSoft.Items;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ItemModels
{
    public interface IItemModelManager : IDefaultActiveValidateServiceBase<ItemModel, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
 
    }
   
}
