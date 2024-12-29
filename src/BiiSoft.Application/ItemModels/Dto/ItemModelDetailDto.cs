using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.ItemModels.Dto
{
    public class ItemModelDetailDto : DefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
    }
}
