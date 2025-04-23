using BiiSoft.Dtos;
using System;

namespace BiiSoft.Zones.Dto
{
    public class FindZoneDto : NameActiveDto<Guid>
    {
        public Guid WarehouseId { get; set; }
        public string WarehouseName { get; set; }
    }
}
