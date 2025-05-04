using System;

namespace BiiSoft.BOMs
{
    public interface IBOMManager : IDefaultActiveValidateServiceBase<BOM, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
 
    }
   
}
