using BiiSoft.Dtos;
using System;

namespace BiiSoft.Zones.Dto
{
    public class FindZoneDto : NameActiveDto<Guid>
    {
        public string WarehouseName { get; set; }
    }
}
