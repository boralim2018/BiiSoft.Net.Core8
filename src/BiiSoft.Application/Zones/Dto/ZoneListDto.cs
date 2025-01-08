using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Zones.Dto
{
    public class ZoneListDto : DefaultNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public string WarehouseName { get; set; }
    }
}
