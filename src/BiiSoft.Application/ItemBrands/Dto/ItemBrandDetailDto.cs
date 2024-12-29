using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.ItemBrands.Dto
{
    public class ItemBrandDetailDto : DefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
    }
}
