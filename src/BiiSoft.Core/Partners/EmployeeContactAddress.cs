using Abp.Timing;
using BiiSoft.ContactInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiiSoft.Partners
{
    public enum EmployeeAddressKeys
    {
        PlaceOfBirthAddress = 1,
        ContactAddress = 2,  
    }

    public class EmployeeContactAddress : ContactAddressBase
    {
        public Guid EmployeeId { get; protected set; }
        public Employee Employee { get; protected set; }
        public EmployeeAddressKeys AddressKey { get; protected set; }

        public static EmployeeContactAddress Create
            (
            int tenantId, 
            long? userId,
            EmployeeAddressKeys key,
            Guid employeeId,
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
            return new EmployeeContactAddress
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                AddressKey = key,
                EmployeeId = employeeId,
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
        
        public EmployeeContactAddress Update
            (
            long? userId,
            EmployeeAddressKeys key,
            Guid employeeId,
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
            EmployeeId = employeeId;
            CountryId = countryId;
            CityProvinceId = cityProvinceId;
            KhanDistrictId = khanDistrictId;
            SangkatCommuneId = sangkatCommuneid;
            VillageId = villageId;
            PostalCode = postalCode;
            Street = street;
            HouseNo = houseNo;   
            
            return this;
        }
    }

}
