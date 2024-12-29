using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Batteries.Dto
{
    public class BatteryListDto : DefaultNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
    }
}
