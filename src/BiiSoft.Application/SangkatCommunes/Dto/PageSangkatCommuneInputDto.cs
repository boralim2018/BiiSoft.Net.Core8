using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.SangkatCommunes.Dto
{
    public class PageSangkatCommuneInputDto : PageAuditedAcitveSortFilterInputDto
    {
        public FilterInputDto<Guid?> Countries { get; set; }
        public FilterInputDto<Guid?> CityProvinces { get; set; }
        public FilterInputDto<Guid?> KhanDistricts { get; set; }

    }

    public class ExportExcelSangkatCommuneInputDto : PageSangkatCommuneInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
