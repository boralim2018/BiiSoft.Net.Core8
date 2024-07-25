using Abp.Timing;
using BiiSoft.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Locations
{
    [Table("BiiCityProvinces")]
    public class CityProvince : LocationBase
    {
        [MaxLength(6)]
        public string ISO { get; private set; }
        public Guid? CountryId { get; private set; }
        public Country Country { get; private set; }

        public static CityProvince Create(long? userId, string code, string name, string displayName, string iso, Guid? countryId, decimal? lat, decimal? lng)
        {
            return new CityProvince
            {
                Id = Guid.NewGuid(),
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Code = code,
                Name = name,
                DisplayName = displayName,
                ISO = iso,
                CountryId = countryId,
                Latitude = lat,
                Longitude = lng,
                IsActive = true,
            };
        }


        public void Update(long? userId, string code, string name, string displayName, string iso, Guid? countryId, decimal? lat, decimal? lng)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Code = code;
            Name = name;
            DisplayName = displayName;          
            ISO = iso;
            Latitude = lat;
            Longitude = lng;
            CountryId = countryId;
        } 
       
    }
}
