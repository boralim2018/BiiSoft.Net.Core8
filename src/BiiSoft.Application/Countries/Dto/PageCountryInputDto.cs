using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Countries.Dto
{
    public class PageCountryInputDto : PageAuditedAcitveSortFilterInputDto
    {
        public FilterInputDto<long> Currencies { get; set; }

    }

    public class ExportExcelCountryInputDto : PageCountryInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
