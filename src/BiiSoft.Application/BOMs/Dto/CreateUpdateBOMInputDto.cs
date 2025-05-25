using BiiSoft.Enums;
using System;
using System.Collections.Generic;

namespace BiiSoft.BOMs.Dto
{
    public class CreateUpdateBOMInputDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public BOMType Type { get; set; }
        public Guid ItemId { get; set; }
        public List<BOMItemDto> BOMItems { get; set; }
    }

}
