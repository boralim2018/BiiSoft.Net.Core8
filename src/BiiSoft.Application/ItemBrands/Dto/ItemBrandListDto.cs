using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.ItemBrands.Dto
{
    public class ItemBrandListDto : DefaultNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
    }
}
