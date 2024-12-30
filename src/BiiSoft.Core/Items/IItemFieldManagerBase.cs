using Abp.Domain.Entities;
using Abp.Domain.Services;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Items
{
    public interface IItemFieldManagerBase<TEntity> : IDefaultActiveValidateServiceBase<TEntity, Guid>, IImporxExcelValidateSerivceBase<Guid> where TEntity : ItemFieldBase
    {
 
    }
   
}
