using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;

namespace BiiSoft.Zones.Dto
{
    public class ZoneDetailDto : DefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
        public Guid WarehouseId { get; set; }
        public string WarehouseName { get; set; }
    }
}
