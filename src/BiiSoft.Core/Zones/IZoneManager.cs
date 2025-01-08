using BiiSoft.Warehouses;
using System;
using System.Threading.Tasks;

namespace BiiSoft.Zones
{
    public interface IZoneManager : IDefaultActiveValidateServiceBase<Zone, Guid>, IImporxExcelValidateSerivceBase<Guid>
    {
        Task<Zone> GetDefaultValueAsync(Guid WarehouseId);
    }
   
}
