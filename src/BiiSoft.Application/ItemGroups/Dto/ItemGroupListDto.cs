using BiiSoft.Dtos;
using System;

namespace BiiSoft.ItemGroups.Dto
{
    public class ItemGroupListDto : DefaultNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
    }
}
