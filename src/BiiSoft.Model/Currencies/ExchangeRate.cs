using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Microsoft.EntityFrameworkCore;
using System;

namespace BiiSoft.Currencies
{
    public class ExchangeRate : AuditedEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long FromCurrencyId { get; protected set; }
        public Currency FromCurrency { get; protected set; }
        public long ToCurrencyId { get; protected set; }
        public Currency ToCurrency { get; protected set; }

        [Precision(18, 8)]
        public decimal BidRate { get; protected set; }
        [Precision(18, 8)]
        public decimal AskRate { get; protected set; }


        public static ExchangeRate Create(int tenantId, long? userId, long fromCurrencyId, long toCurrencyId, decimal bidRate, decimal askRate )
        {
            return new ExchangeRate
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                FromCurrencyId = fromCurrencyId,
                ToCurrencyId = toCurrencyId,
                BidRate = bidRate,
                AskRate = askRate,
            };
        }

        public static ExchangeRate Create(int tenantId, long? userId, Currency fromCurrency, Currency toCurrency, decimal bidRate, decimal askRate)
        {
            return new ExchangeRate
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                FromCurrency = fromCurrency,
                ToCurrency = toCurrency,
                BidRate = bidRate,
                AskRate = askRate,
            };
        }


        public void Update(long? userId, long fromCurrencyId, long toCurrencyId, decimal bidRate, decimal askRate)
        {

            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            FromCurrencyId = fromCurrencyId;
            ToCurrencyId = toCurrencyId;
            BidRate = bidRate;
            AskRate = askRate;
        }

    }
}
