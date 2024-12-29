using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.FieldBs.Dto
{
    public class FieldBDetailDto : DefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
    }
}
