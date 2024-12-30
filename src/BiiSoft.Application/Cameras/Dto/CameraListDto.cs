using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Cameras.Dto
{
    public class CameraListDto : DefaultNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public string Code { get; set; }
    }
}
