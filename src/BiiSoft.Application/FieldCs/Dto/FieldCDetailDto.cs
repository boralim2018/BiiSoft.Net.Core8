using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.FieldCs.Dto
{
    public class FieldCDetailDto : DefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
    }
}
