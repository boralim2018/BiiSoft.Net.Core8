using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.ItemSizes.Dto
{
    public class ItemSizeListDto : DefaultNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public string Code { get; set; }
    }
}
