using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.FieldAs.Dto
{
    public class FieldADetailDto : DefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
    }
}
