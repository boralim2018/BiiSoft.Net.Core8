using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.ItemModels.Dto
{
    public class ItemModelListDto : DefaultNameActiveAuditedDto<Guid>
    {      
        public long No { get; set; }
    }
}
