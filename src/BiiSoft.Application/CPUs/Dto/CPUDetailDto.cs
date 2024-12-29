using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.CPUs.Dto
{
    public class CPUDetailDto : DefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
    }
}
