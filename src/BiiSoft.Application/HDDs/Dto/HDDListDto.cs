using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.HDDs.Dto
{
    public class HDDListDto : DefaultNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
    }
}
