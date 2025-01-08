using BiiSoft.Columns;
using BiiSoft.Dtos;
using System;
using System.Collections.Generic;

namespace BiiSoft.Zones.Dto
{
    public class PageZoneInputDto : PageAuditedAcitveSortFilterInputDto
    {
        public FilterInputDto<Guid> WarehouseFilter { get; set; }

        protected override string MapSortField()
        {
            return SortField switch
            {
                "WarehouseName" => "Warehouse.Name",
                _ => base.MapSortField()
            };
        }
    }

    public class FindZoneInputDto : PageZoneInputDto
    {
        
    }

    public class ExportExcelZoneInputDto : PageZoneInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
