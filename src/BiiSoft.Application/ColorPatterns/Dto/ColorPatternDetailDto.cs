using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.ColorPatterns.Dto
{
    public class ColorPatternDetailDto : DefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
        public string Code { get; set; }
    }
}
