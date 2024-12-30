using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.FieldCs.Dto
{
    public class FieldCListDto : DefaultNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public string Code { get; set; }
    }
}
