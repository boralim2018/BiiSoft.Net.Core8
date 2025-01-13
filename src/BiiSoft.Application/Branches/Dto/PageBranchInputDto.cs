using BiiSoft.Auditing.Dto;
using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Branches.Dto
{
    public class PageBranchInputDto : PageAuditedAcitveSortFilterInputDto
    {
        protected override string MapSortField()
        {
            return SortField switch
            {
                "SharingName" => "Sharing",
                _ => base.MapSortField()
            };
        }
    }

    public class ExportExcelBranchInputDto : PageBranchInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
