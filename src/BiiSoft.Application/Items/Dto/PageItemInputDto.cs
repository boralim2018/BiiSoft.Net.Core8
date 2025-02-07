using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;

namespace BiiSoft.Items.Dto
{
    public class PageItemInputDto : PageAuditedAcitveSortFilterInputDto
    {
        public FilterInputDto<ItemType> ItemTypeFilter { get; set; }
        public FilterInputDto<ItemCategory> ItemCategoryFilter { get; set; }
        public FilterInputDto<Guid> UnitFilter { get; set; }
        public FilterInputDto<Guid> ItemGroupFilter { get; set; }
        public FilterInputDto<Guid> ItemBrandFilter { get; set; }
        public FilterInputDto<Guid> ItemGradeFilter { get; set; }
        public FilterInputDto<Guid> ItemModelFilter { get; set; }
        public FilterInputDto<Guid> ItemSizeFilter { get; set; }
        public FilterInputDto<Guid> ItemSeriesFilter { get; set; }
        public FilterInputDto<Guid> ColorPatternFilter { get; set; }
        public FilterInputDto<Guid> CPUFilter { get; set; }
        public FilterInputDto<Guid> RAMFilter { get; set; }
        public FilterInputDto<Guid> VGAFilter { get; set; }
        public FilterInputDto<Guid> HDDFilter { get; set; }
        public FilterInputDto<Guid> ScreenFilter { get; set; }
        public FilterInputDto<Guid> CameraFilter { get; set; }
        public FilterInputDto<Guid> BatteryFilter { get; set; }
        public FilterInputDto<Guid> FieldAFilter { get; set; }
        public FilterInputDto<Guid> FieldBFilter { get; set; }
        public FilterInputDto<Guid> FieldCFilter { get; set; }
        protected override string MapSortField()
        {
            return SortField switch
            {
                "ItemTypeName" => "ItemType",
                "ItemCategoryName" => "ItemCategory",
                "ItemGroupName" => "ItemGroup.Name",
                "ItemGradeName" => "ItemGrade.Name",
                "ItemBrandName" => "ItemBrand.Name",
                "ItemModelName" => "ItemModel.Name",
                "ItemSizeName" => "ItemSize.Name",
                "ItemSeriesName" => "ItemSeriea.Name",
                "ColorPatternName" => "ColorPattern.Name",
                "UnitName" => "Unit.Name",
                "CUPName" => "CPU.Name",
                "RAMName" => "RAM.Name",
                "VGAName" => "VGA.Name",
                "HDDName" => "HDD.Name",
                "ScreenName" => "Screen.Name",
                "CameraName" => "Camera.Name",
                "BatteryName" => "Battery.Name",
                "FieldAName" => "FieldA.Name",
                "FieldBName" => "FieldB.Name",
                "FieldCName" => "FieldC.Name",
                _ => base.MapSortField()
            };
        }
    }

    public class ExportExcelItemInputDto : PageItemInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
