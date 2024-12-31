using Abp.Timing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Items
{
    [Table("BiiUnits")]
    public class Unit : ItemFieldBase
    {
        public static Unit Create(int tenantId, long userId, string name, string displayName, string code)
        {
            return new Unit
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Code = code,
                IsActive = true
            };
        }

    }
}
