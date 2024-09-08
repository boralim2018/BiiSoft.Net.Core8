using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Locations
{
    [Table("BiiKhanDistricts")]
    public class KhanDistrict : CanModifyNameActiveEntity<Guid>, IMustHaveTenant, INoEntity
    {
        public int TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
        [MaxLength(9)]
        public string Code { get; private set; }
        public Guid? CountryId { get; private set; }
        public Country Country { get; private set; }
        public Guid? CityProvinceId { get; private set; }
        public CityProvince CityProvince { get; private set; }


        public static KhanDistrict Create(long? userId, string code, string name, string displayName, Guid? countryId, Guid? cityProvinceId)
        {
            return new KhanDistrict
            {
                Id = Guid.NewGuid(),
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Code = code,
                Name = name,
                DisplayName = displayName,  
                CountryId = countryId,
                CityProvinceId = cityProvinceId,
                IsActive = true,
            };
        }


        public void Update(long? userId, string code, string name, string displayName, Guid? countryId, Guid? cityProvinceId)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Code = code;
            Name = name;
            DisplayName = displayName;   
            CountryId = countryId;
            CityProvinceId = cityProvinceId;
        } 
       
    }
}
