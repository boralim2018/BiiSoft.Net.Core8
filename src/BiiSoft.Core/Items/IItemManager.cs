using System;

namespace BiiSoft.Items
{
    public interface IItemManager : IActiveValidateServiceBase<Item, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
  
    }
   
}
