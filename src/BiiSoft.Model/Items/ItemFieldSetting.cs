using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Items
{
    [Table("BiiItemFieldSettings")]
    public class ItemFieldSetting : ActiveEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public bool UseCode { get; private set; }
        
        public static ItemFieldSetting Create(
            int tenantId,
            long userId,
            bool useCode)
        {
            return new ItemFieldSetting
            {
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                UseCode = useCode,
                IsActive = true
            };
        }

        public void Update(
            long userId,
            bool useCode
          )
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            UseCode = useCode;
        }
    }
}
