using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Screens.Dto
{
    public class ScreenListDto : DefaultNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
    }
}
