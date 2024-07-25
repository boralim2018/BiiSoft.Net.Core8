using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Branches.Dto
{
    public class BranchListDto : DefaultNameActiveAuditedDto<Guid>
    {
        public long No { get; set; }
        public string BusinessId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string TaxRegistrationNumber { get; set; }
        public string CountryName { get; set; }
        public string CityProvinceName { get; set; }
        public string KhanDistrictName { get; set; }
        public string SangkatCommuneName { get; set; }
        public string VillageName { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string HouseNo { get; set; }
    }
}
