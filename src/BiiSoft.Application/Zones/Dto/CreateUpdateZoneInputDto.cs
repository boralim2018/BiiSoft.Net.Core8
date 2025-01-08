using BiiSoft.Enums;
using System;
using System.Collections.Generic;

namespace BiiSoft.Zones.Dto
{
    public class CreateUpdateZoneInputDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public Guid WarehouseId { get; set; }
    }

}
