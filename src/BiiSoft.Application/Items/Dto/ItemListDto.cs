using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Items.Dto
{
    public class ItemListDto : NameActiveAuditedDto<Guid>
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
        public string ItemGroupName { get; set; }
        public string ItemBrandName { get; set; }
        public string ItemGradeName { get; set; }
        public string ItemSizeName { get; set; }
        public string UnitName { get; set; }
        public string ColorPatternName { get; set; }
        public string ItemSeriesName { get; set; }
        public string ItemModelName { get; set; }
        public string CPUName { get; set; }
        public string RAMName { get; set; }
        public string VGAName { get; set; }
        public string HDDName { get; set; }
        public string BatteryName { get; set; }
        public string CameraName { get; set; }
        public string ScreenName { get; set; }
        public string FieldAName { get; set; }
        public string FieldBName { get; set; }
        public string FieldCName { get; set; }
        public string Description { get; set; }

    }
}
