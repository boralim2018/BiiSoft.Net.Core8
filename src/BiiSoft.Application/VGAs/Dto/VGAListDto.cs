using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.VGAs.Dto
{
    public class VGAListDto : DefaultNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public string Code { get; set; }
    }
}
