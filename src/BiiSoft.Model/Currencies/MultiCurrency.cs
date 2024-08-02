using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using BiiSoft.Entities;

namespace BiiSoft.Currencies
{
    public class MultiCurrency : DefaultActiveEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long CurrencyId { get; protected set; }
        public Currency Currency { get; protected set; }


        public static MultiCurrency Create(int tenantId, long? userId, long currencyId)
        {
            return new MultiCurrency
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                CurrencyId = currencyId
            };
        }

    }
}
