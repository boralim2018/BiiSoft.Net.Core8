using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Batteries.Dto
{
    public class BatteryDetailDto : DefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
    }
}
