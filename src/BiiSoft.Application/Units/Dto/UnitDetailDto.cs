using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Units.Dto
{
    public class UnitDetailDto : DefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
        public string Code { get; set; }
    }
}
