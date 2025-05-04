using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;

namespace BiiSoft.BOMs.Dto
{
    public class BOMDetailDto : DefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {      
        public long No { get; set; }
        public BOMType Type { get; set; }
        public string TypeName { get; set; }
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public List<BOMItemDto> BOMItems { get; set; }
    }
}
