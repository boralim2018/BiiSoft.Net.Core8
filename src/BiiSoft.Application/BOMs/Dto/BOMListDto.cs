using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.BOMs.Dto
{
    public class BOMListDto : DefaultNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
        public BOMType Type { get; set; }
        public string TypeName { get; set; }
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
    }
}
