using BiiSoft.Dtos;
using System;

namespace BiiSoft.Batteries.Dto
{
    public class BatteryDetailDto : DefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
        public string Code { get; set; }
    }
}
