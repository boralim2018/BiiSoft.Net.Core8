using System;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using BiiSoft.Locations;

namespace BiiSoft.ContactInfo
{
    
    public class ContactAddress : AuditedEntity<Guid>, IMustHaveTenant
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

        public static ContactAddress Create(
            int tenantId,
            long? userId,
            Guid? countryId,
            Guid? cityProvinceId,
            Guid? khanDistrictId,
            Guid? sangkatCommuneid,
            Guid? villageId,
            Guid? locationId,
            string postalCode,
            string street,
            string houseNo
            )
        {
            return new ContactAddress
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                CountryId = countryId,
                CityProvinceId = cityProvinceId,
                KhanDistrictId = khanDistrictId,
                SangkatCommuneId = sangkatCommuneid,
                VillageId = villageId,
                LocationId = locationId,
                PostalCode = postalCode,
                Street = street,
                HouseNo = houseNo,
            };
        }

        public ContactAddress Update(
            long? userId,
            Guid? countryId,
            Guid? cityProvinceId,
            Guid? khanDistrictId,
            Guid? sangkatCommuneid,
            Guid? villageId,
            Guid? locationId,
            string postalCode,
            string street,
            string houseNo
            )
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            CountryId = countryId;
            CityProvinceId = cityProvinceId;
            KhanDistrictId = khanDistrictId;
            SangkatCommuneId = sangkatCommuneid;
            VillageId = villageId;
            LocationId = locationId;
            PostalCode = postalCode;
            Street = street;
            HouseNo = houseNo;

            return this;
        }

    }

}
