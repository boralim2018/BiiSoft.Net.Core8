using BiiSoft.Columns;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.CityProvinces.Dto
{
    public class PageCityProvinceInputDto : PageAuditedAcitveSortFilterInputDto
    {
        public FilterInputDto<Guid?> Countries { get; set; }

        protected override string MapSortField()
        {
            return SortField switch
            {
                "CountryName" => "Country.Name",
                _ => base.MapSortField()
            };
        }
    }

    public class ExportExcelCityProvinceInputDto : PageCityProvinceInputDto
    {
        public List<ColumnOutput> Columns { get; set; }
    }
}
