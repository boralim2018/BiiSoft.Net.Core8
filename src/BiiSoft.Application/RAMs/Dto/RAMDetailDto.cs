using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.RAMs.Dto
{
    public class RAMDetailDto : DefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
        public string Code { get; set; }
    }
}
