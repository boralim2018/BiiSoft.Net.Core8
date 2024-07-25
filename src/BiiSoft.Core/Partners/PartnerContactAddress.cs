using Abp.Timing;
using BiiSoft.ContactInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiiSoft.Partners
{
    public enum PartnerAddressKeys
    {
        BillingAddress = 1,
        ShippingAddress = 2,  
    }

    public class PartnerContactAddress : ContactAddressBase
    {
        public Guid PartnerId { get; protected set; }
        public Partner Partner { get; protected set; }
        public PartnerAddressKeys AddressKey { get; protected set; }

        public static PartnerContactAddress Create
            (
            int tenantId, 
            long? userId,
            PartnerAddressKeys key,
            Guid partnerId,
            Guid? countryId,
            Guid? cityProvinceId,
            Guid? khanDistrictId, 
            Guid? sangkatCommuneid, 
            Guid? villageId, 
            string postalCode,
            string street,
            string houseNo
            )
        {
            return new PartnerContactAddress
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                AddressKey = key,
                PartnerId = partnerId,
                CountryId = countryId,
                CityProvinceId = cityProvinceId,
                KhanDistrictId = khanDistrictId,
                SangkatCommuneId = sangkatCommuneid,
                VillageId = villageId,
                PostalCode = postalCode,
                Street = street,
                HouseNo = houseNo,                
            };
        }
        
        public void Update
            (
            long? userId,
            PartnerAddressKeys key,
            Guid partnerId,
            Guid? countryId, 
            Guid? cityProvinceId, 
            Guid? khanDistrictId, 
            Guid? sangkatCommuneid, 
            Guid? villageId, 
            string postalCode,
            string street,
            string houseNo
            )
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            AddressKey = key;
            PartnerId = partnerId;
            CountryId = countryId;
            CityProvinceId = cityProvinceId;
            KhanDistrictId = khanDistrictId;
            SangkatCommuneId = sangkatCommuneid;
            VillageId = villageId;
            PostalCode = postalCode;
            Street = street;
            HouseNo = houseNo;   
        }
    }

}
