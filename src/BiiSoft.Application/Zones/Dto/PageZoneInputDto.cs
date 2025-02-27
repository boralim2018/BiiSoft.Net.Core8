using BiiSoft.Columns;
using BiiSoft.Dtos;
using System;
using System.Collections.Generic;

namespace BiiSoft.Zones.Dto
{
    public class PageZoneInputDto : PageAuditedAcitveSortFilterInputDto
    {
        public FilterInputDto<Guid> WarehouseFilter { get; set; }

    }

    public class FindZoneInputDto : PageZoneInputDto
    {
        
    }

    public class ExportExcelZoneInputDto : PageZoneInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
