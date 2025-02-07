using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;

namespace BiiSoft.Items.Dto
{
    public class ItemDetailDto : NameActiveAuditedNavigationDto<Guid>, INoDto
    {
        public long No { get; set; }
        public ItemType ItemType { get; set; }
        public string ItemTypeName { get; set; }
        public ItemCategory ItemCategory { get; set; }
        public string ItemCategoryName { get; set; }
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
        public string WeightUnitName { get; set; }
        public LengthUnit LengthUnit { get; set; }
        public string LengthUnitName { get; set; }
        public AreaUnit AreaUnit { get; set; }
        public string AreaUnitName { get; set; }
        public VolumeUnit VolumeUnit { get; set; }
        public string VolumeUnitName { get; set; }

        public bool TrackSerial { get; set; }
        public bool TrackExpired { get; set; }
        public bool TrackBatchNo { get; set; }
        public bool TrackInventoryStatus { get; set; }

        public decimal ReorderStock { get; set; }
        public decimal MinStock { get; set; }
        public decimal MaxStock { get; set; }

        public Guid? ItemGroupId { get; set; }
        public string ItemGroupName { get; set; }
        public Guid? ItemBrandId { get; set; }
        public string ItemBrandName { get; set; }
        public Guid? ItemGradeId { get; set; }
        public string ItemGradeName { get; set; }
        public Guid? ItemSizeId { get; set; }
        public string ItemSizeName { get; set; }
        public Guid? UnitId { get; set; }
        public string UnitName { get; set; }
        public Guid? ColorPatternId { get; set; }
        public string ColorPatternName { get; set; }
        public Guid? ItemSeriesId { get; set; }
        public string ItemSeriesName { get; set; }
        public Guid? ItemModelId { get; set; }
        public string ItemModelName { get; set; }
        public Guid? CPUId { get; set; }
        public string CPUName { get; set; }
        public Guid? RAMId { get; set; }
        public string RAMName { get; set; }
        public Guid? VGAId { get; set; }
        public string VGAName { get; set; }
        public Guid? HDDId { get; set; }
        public string HDDName { get; set; }
        public Guid? BatteryId { get; set; }
        public string BatteryName { get; set; }
        public Guid? CameraId { get; set; }
        public string CameraName { get; set; }
        public Guid? ScreenId { get; set; }
        public string ScreenName { get; set; }
        public Guid? FieldAId { get; set; }
        public string FieldAName { get; set; }
        public Guid? FieldBId { get; set; }
        public string FieldBName { get; set; }
        public Guid? FieldCId { get; set; }
        public string FieldCName { get; set; }

        public Guid? ImageId { get; set; }

        public Guid? PurchaseAccountId { get; set; }
        public string PurchaseAccountName { get; set; }
        public Guid? SaleAccountId { get; set; }
        public string SaleAccountName { get; set; }
        public Guid? InventoryAccountId { get; set; }
        public string InventoryAccountName { get; set; }
        public string Description { get; set; }
        public List<ItemZoneDto> ItemZones { get; set; }
    }
}
