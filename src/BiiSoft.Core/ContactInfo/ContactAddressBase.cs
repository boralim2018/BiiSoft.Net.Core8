using System;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using BiiSoft.Enums;
using BiiSoft.Locations;

namespace BiiSoft.ContactInfo
{
    

    public abstract class ContactAddressBase<TPrimaryKey> : AuditedEntity<TPrimaryKey>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public Guid? CountryId { get; protected set; }
        public Country Country { get; protected set; }        

        public Guid? CityProvinceId { get; protected set; }
        public CityProvince CityProvince { get; protected set; }

        public Guid? KhanDistrictId { get; protected set; }
        public KhanDistrict KhanDistrict { get; protected set; }

        public Guid? SangkatCommuneId { get; protected set; }
        public SangkatCommune SangkatCommune { get; protected set; }

        public Guid? VillageId { get; protected set; }
        public Village Village { get; protected set; }

        public string PostalCode { get; protected set; }
        public string Street { get; protected set; }
        public string HouseNo { get; protected set; }

        public Guid? LocationId { get; protected set; }
        public Location Location { get; protected set; }
        public void SetLocation(Guid? locationId) => LocationId = locationId;
        public AddressType AddressType { get; protected set; }
    }

    public abstract class ContactAddressBase : ContactAddressBase<Guid>
    {

    }
}
