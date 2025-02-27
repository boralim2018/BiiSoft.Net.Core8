using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.KhanDistricts.Dto
{
    public class PageKhanDistrictInputDto : PageAuditedAcitveSortFilterInputDto
    {
        public FilterInputDto<Guid?> Countries { get; set; }
        public FilterInputDto<Guid?> CityProvinces { get; set; }

    }

    public class ExportExcelKhanDistrictInputDto : PageKhanDistrictInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
