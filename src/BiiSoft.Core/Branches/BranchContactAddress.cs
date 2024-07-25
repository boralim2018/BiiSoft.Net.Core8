using Abp.Timing;
using BiiSoft.ContactInfo;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Branches
{
    [Table("BiiBranchContactAddresses")]
    public class BranchContactAddress : ContactAddressBase
    {
        public Guid BranchId { get; protected set; }
        public Branch Branch { get; protected set; }
        public void SetBranch(Guid branchId) => BranchId = branchId; 
        public bool IsDefault { get; protected set; }
        public void SetDefault(bool isDefault) => IsDefault = isDefault;

        public static BranchContactAddress Create(int tenantId, long? userId, Guid? countryId)
        {
            return new BranchContactAddress
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,              
                CreationTime = Clock.Now,
                CountryId = countryId
            };
        }

        public static BranchContactAddress Create (
            int tenantId,
            long? userId,
            Guid branchId,
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
            return new BranchContactAddress
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                BranchId = branchId,
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

        public BranchContactAddress Update (
            long? userId,
            Guid branchId,
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
            BranchId = branchId;
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
