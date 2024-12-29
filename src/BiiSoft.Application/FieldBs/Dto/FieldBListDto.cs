using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.FieldBs.Dto
{
    public class FieldBListDto : DefaultNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
    }
}
