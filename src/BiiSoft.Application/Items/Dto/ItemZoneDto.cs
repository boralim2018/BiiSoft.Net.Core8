using System;

namespace BiiSoft.Items.Dto
{
    public class ItemZoneDto
    {
        public Guid? Id { get; set; }
        public Guid ZoneId { get; set; }
        public string ZoneName { get; set; }
        public Guid WarehouseId { get; set; }

    }
}
