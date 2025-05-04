using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;

namespace BiiSoft.BOMs.Dto
{
    public class PageBOMInputDto : PageAuditedAcitveSortFilterInputDto
    {
        public FilterInputDto<BOMType> TypeFilter { get; set; }
        public FilterInputDto<Guid> ItemFilter { get; set; }

    }

    public class FindBOMInputDto : PageBOMInputDto
    {
        
    }

    public class ExportExcelBOMInputDto : PageBOMInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
