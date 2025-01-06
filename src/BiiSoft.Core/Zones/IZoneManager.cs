using BiiSoft.Warehouses;
using System;

namespace BiiSoft.Zones
{
    public interface IZoneManager : IDefaultActiveValidateServiceBase<Zone, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
 
    }
   
}
