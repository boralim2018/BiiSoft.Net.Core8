using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Villages.Dto
{
    public class CreateUpdateVillageInputDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Code { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? CityProvinceId { get; set; }
        public Guid? KhanDistrictId { get; set; }
        public Guid? SangkatCommuneId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }

}
