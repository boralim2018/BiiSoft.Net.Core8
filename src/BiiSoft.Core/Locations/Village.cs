using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Locations
{
    [Table("BiiVillages")]
    public class Village : CanModifyNameActiveEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
        [MaxLength(15)]
        public string Code { get; private set; }
        public Guid? CountryId { get; private set; }
        public Country Country { get; private set; }
        public Guid? CityProvinceId { get; private set; }
        public CityProvince CityProvince { get; private set; }
        public Guid? KhanDistrictId { get; private set; }
        public KhanDistrict KhanDistrict { get; private set; }
        public Guid? SangkatCommuneId { get; private set; }
        public SangkatCommune SangkatCommune { get; private set; }


        public static Village Create(int tenantId, long? userId, string code, string name, string displayName, Guid? countryId, Guid? cityProvinceId, Guid? khanDistrictId, Guid? sangkatCommuneId)
        {
            return new Village
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Code = code,
                CountryId = countryId,
                CityProvinceId = cityProvinceId,
                KhanDistrictId = khanDistrictId,          
                SangkatCommuneId = sangkatCommuneId,
                IsActive = true,
            };
        }


        public void Update(long? userId, string code, string name, string displayName, Guid? countryId, Guid? cityProvinceId, Guid? khanDistrictId, Guid? sangkatCommuneId)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Code = code;
            Name = name;
            DisplayName = displayName;   
            CountryId = countryId;
            CityProvinceId = cityProvinceId;
            KhanDistrictId = khanDistrictId;
            SangkatCommuneId = sangkatCommuneId;
        } 
       
    }
}
