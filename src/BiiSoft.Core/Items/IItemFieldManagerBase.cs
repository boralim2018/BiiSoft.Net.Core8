using System;

namespace BiiSoft.Items
{
    public interface IItemFieldManagerBase<TEntity> : IDefaultActiveValidateServiceBase<TEntity, Guid>, IImporxExcelValidateSerivceBase<Guid> where TEntity : ItemFieldBase
    {
 
    }
   
}
