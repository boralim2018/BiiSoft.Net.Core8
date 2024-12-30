using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Screens.Dto
{
    public class ScreenDetailDto : DefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
        public string Code { get; set; }
    }
}
