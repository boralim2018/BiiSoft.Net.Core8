using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Units.Dto
{
    public class UnitListDto : DefaultNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public string Code { get; set; }
    }
}
