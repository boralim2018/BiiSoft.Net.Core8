﻿using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.KhanDistricts.Dto
{
    public class CreateUpdateKhanDistrictInputDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Code { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? CityProvinceId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }

}
