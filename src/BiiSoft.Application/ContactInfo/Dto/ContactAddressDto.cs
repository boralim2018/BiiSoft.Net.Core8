using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ContactInfo.Dto
{
    public class ContactAddressDto: EntityDto<Guid>
    {
        public Guid? CountryId { get; set; }
        public string CountryName { get; set; }
        public Guid? CityProvinceId { get;set; }
        public string CityProvinceName { get; set; }
        public Guid? KhanDistrictId { get; set; }   
        public string KhanDistrictName { get; set; }
        public Guid? SangkatCommuneId { get; set; }
        public string SangkatCommuneName { get; set; }
        public Guid? VillageId { get; set; }
        public string VillageName { get; set; }
        public Guid? LocationId { get; set; }
        public string LocationName { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string HouseNo { get; set; }
    }
}
