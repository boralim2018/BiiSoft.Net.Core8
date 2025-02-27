using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Villages.Dto
{
    public class PageVillageInputDto : PageAuditedAcitveSortFilterInputDto
    {
        public FilterInputDto<Guid?> Countries { get; set; }
        public FilterInputDto<Guid?> CityProvinces { get; set; }
        public FilterInputDto<Guid?> KhanDistricts { get; set; }
        public FilterInputDto<Guid?> SangkatCommunes { get; set; }

    }

    public class ExportExcelVillageInputDto : PageVillageInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
