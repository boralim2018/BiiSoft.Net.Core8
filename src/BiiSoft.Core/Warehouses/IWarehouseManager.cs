using System;

namespace BiiSoft.Warehouses
{
    public interface IWarehouseManager : IDefaultActiveValidateServiceBase<Warehouse, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
 
    }
   
}
