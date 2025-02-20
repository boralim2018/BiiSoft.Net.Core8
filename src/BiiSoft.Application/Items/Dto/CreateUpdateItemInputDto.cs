using BiiSoft.Enums;
using System;
using System.Collections.Generic;

namespace BiiSoft.Items.Dto
{
    public class CreateUpdateItemInputDto
    {
        public Guid? Id { get; set; }
        public long No { get; set; }
        public ItemType ItemType { get; set; }
        public ItemCategory ItemCategory { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Code { get; set; }

        public decimal NetWeight { get; set; }
        public decimal GrossWeight { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Diameter { get; set; }
        public decimal Area { get; set; }
        public decimal Volume { get; set; }

        public WeightUnit WeightUnit { get; set; }
        public LengthUnit LengthUnit { get; set; }
        public AreaUnit AreaUnit { get; set; }
        public VolumeUnit VolumeUnit { get; set; }

        public bool TrackSerial { get; set; }
        public bool TrackExpired { get; set; }
        public bool TrackBatchNo { get; set; }
        public bool TrackInventoryStatus { get; set; }

        public decimal ReorderStock { get; set; }
        public decimal MinStock { get; set; }
        public decimal MaxStock { get; set; }

        public Guid? ItemGroupId { get; set; }
        public Guid? ItemBrandId { get; set; }
        public Guid? ItemGradeId { get; set; }
        public Guid? ItemSizeId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? ColorPatternId { get; set; }
        public Guid? ItemSeriesId { get; set; }
        public Guid? ItemModelId { get; set; }
        public Guid? CPUId { get; set; }
        public Guid? RAMId { get; set; }
        public Guid? VGAId { get; set; }
        public Guid? HDDId { get; set; }
        public Guid? BatteryId { get; set; }
        public Guid? CameraId { get; set; }
        public Guid? ScreenId { get; set; }
        public Guid? FieldAId { get; set; }
        public Guid? FieldBId { get; set; }
        public Guid? FieldCId { get; set; }

        public Guid? ImageId { get; set; }

        public Guid? PurchaseAccountId { get; set; }
        public Guid? SaleAccountId { get; set; }
        public Guid? InventoryAccountId { get; set; }
        public string Description { get; set; }
        public List<ItemZoneDto> ItemZones { get; set; }
    }

}
