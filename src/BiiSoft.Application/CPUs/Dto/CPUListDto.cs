using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.CPUs.Dto
{
    public class CPUListDto : DefaultNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public string Code { get; set; }
    }
}
