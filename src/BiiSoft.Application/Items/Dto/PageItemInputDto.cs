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
        
    }

    public class ExportExcelItemInputDto : PageItemInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
