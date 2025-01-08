using BiiSoft.Columns;
using BiiSoft.Dtos;
using System;
using System.Collections.Generic;

namespace BiiSoft.Warehouses.Dto
{
    public class PageWarehouseInputDto : PageAuditedAcitveSortFilterInputDto
    {
        public FilterInputDto<Guid> BranchFilter { get; set; }

        protected override string MapSortField()
        {
            return SortField switch
            {
                "SharingName" => "Sharing",
                _ => base.MapSortField()
            };
        }
    }

    public class FindWarehouseInputDto : PageWarehouseInputDto
    {
        
    }

    public class ExportExcelWarehouseInputDto : PageWarehouseInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
