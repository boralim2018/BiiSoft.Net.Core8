using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using BiiSoft.Currencies;
using System;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using BiiSoft.Locations;
using BiiSoft.Enums;

namespace BiiSoft.Branches
{

    [Table("BiiCompanyGeneralSettings")]
    public class CompanyGeneralSetting : AuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public Guid? CountryId { get; protected set; }
        public Country Country { get; protected set; }
        public string DefaultTimeZone { get; protected set; }
        public long? CurrencyId { get; protected set; }
        public Currency Currency { get; protected set; }
        public DateTime? BusinessStartDate { get; protected set; }

        public int RoundTotalDigits { get; protected set; }
        public int RoundCostDigits { get; protected set; }

        public AddressLevel ContactAddressLevel { get; protected set; }
        public void SetAddressLevel(AddressLevel addressLevel) => ContactAddressLevel = addressLevel;

        /*** required one default branch ***/

        public static CompanyGeneralSetting Create(
            int tenantId,
            long? userId,
            Guid? countryId,
            string defaultTimeZone,
            long? currencyId,
            DateTime? businessStartDate,
            int roundTotalDigits,
            int roundCostDigts,
            AddressLevel contactAddressLevel)
        {
            return new CompanyGeneralSetting
            {
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                CountryId = countryId,
                DefaultTimeZone = defaultTimeZone,
                CurrencyId = currencyId,
                BusinessStartDate = businessStartDate,
                RoundTotalDigits = roundTotalDigits,
                RoundCostDigits = roundCostDigts,
                ContactAddressLevel = contactAddressLevel
            };
        }

        public void Update(
            long? userId,
            Guid? countryId,
            string defaultTimeZone,
            long? currencyId,
            DateTime? businessStartDate,
            int roundTotalDigits,
            int roundCostDigts,
            AddressLevel contactAddressLevel)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            CountryId = countryId;
            DefaultTimeZone = defaultTimeZone;
            CurrencyId = currencyId;
            BusinessStartDate = businessStartDate;
            RoundTotalDigits = roundTotalDigits;
            RoundCostDigits = roundCostDigts;
            ContactAddressLevel = contactAddressLevel;
        }

    }
}
