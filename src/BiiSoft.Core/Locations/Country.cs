using Abp.Timing;
using BiiSoft.Currencies;
using BiiSoft.Entities;
using BiiSoft.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Locations
{
    [Table("BiiCountries")]
    public  class Country : LocationBase
    {
        [MaxLength(3)]
        public int CountryCode { get; private set; }
        [MaxLength(2)]
        public string ISO2 { get; private set; }
        [MaxLength(3)]
        public string ISO { get; private set; }

        [MaxLength(BiiSoftConsts.MaxLengthCode)]
        public string PhonePrefix { get; private set; }
       
        public long? CurrencyId { get; private set; }
        public Currency Currency { get; private set; }
        public void SetCurrency(long? currencyId) => CurrencyId = currencyId;

        public static Country Create(long? userId, string locationCode, int countryCode, string name, string displayName, string iso2, string iso, string phonePrefix, long? currencyId, decimal? lat, decimal? lng)
        {
            return new Country
            {
                Id = Guid.NewGuid(),
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Code = locationCode,
                CountryCode = countryCode,
                Name = name,
                DisplayName = displayName,    
                ISO2 = iso2,
                ISO = iso,
                PhonePrefix = phonePrefix,
                CurrencyId = currencyId,
                Latitude = lat,
                Longitude = lng,
                IsActive = true,
            };
        }


        public void Update(long? userId, string locationCode, int countryCode, string name, string displayName, string iso2, string iso, string phonePrefix, long? currencyId, decimal? lat, decimal? lng)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Code = locationCode;
            CountryCode = countryCode;
            Name = name;
            DisplayName = displayName;     
            ISO2 = iso2;
            ISO = iso;
            PhonePrefix = phonePrefix;
            CurrencyId = currencyId;
            Latitude = lat;
            Longitude = lng;
        } 
       
    }
}
