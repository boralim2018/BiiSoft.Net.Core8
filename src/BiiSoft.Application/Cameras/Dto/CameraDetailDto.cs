using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Cameras.Dto
{
    public class CameraDetailDto : DefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
    }
}
