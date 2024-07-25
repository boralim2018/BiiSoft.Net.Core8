using Abp.Timing;
using BiiSoft.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Locations
{
    [Table("BiiSangkatCommunes")]
    public class SangkatCommune : LocationBase
    {
        public Guid? CountryId { get; private set; }
        public Country Country { get; private set; }
        public Guid? CityProvinceId { get; private set; }
        public CityProvince CityProvince { get; private set; }
        public Guid? KhanDistrictId { get; private set; }
        public KhanDistrict KhanDistrict { get; private set; }


        public static SangkatCommune Create(long? userId, string code, string name, string displayName, Guid? countryId, Guid? cityProvinceId, Guid? khanDistrictId, decimal? lat, decimal? lng)
        {
            return new SangkatCommune
            {
                Id = Guid.NewGuid(),
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Code = code,
                Name = name,
                DisplayName = displayName,     
                CountryId = countryId,
                CityProvinceId = cityProvinceId,
                KhanDistrictId = khanDistrictId,        
                Latitude = lat,
                Longitude = lng,
                IsActive = true,
            };
        }


        public void Update(long? userId, string code, string name, string displayName, Guid? countryId, Guid? cityProvinceId, Guid? khanDistrictId, decimal? lat, decimal? lng)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Code = code;
            Name = name;
            DisplayName = displayName;    
            CountryId = countryId;
            CityProvinceId = cityProvinceId;
            KhanDistrictId = khanDistrictId;
            Latitude = lat;
            Longitude = lng;
        } 
       
    }
}
