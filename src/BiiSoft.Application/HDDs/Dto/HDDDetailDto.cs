using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.HDDs.Dto
{
    public class HDDDetailDto : DefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
        public string Code { get; set; }
    }
}
