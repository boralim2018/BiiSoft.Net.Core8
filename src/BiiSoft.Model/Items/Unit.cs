using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Timing;
using BiiSoft.Entities;

namespace BiiSoft.Items
{
    [Table("BiiUnits")]
    public class Unit : DefaultNameActiveEntity<Guid>, IMustHaveTenant, INoEntity
    {
        public int TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
        
        public static Unit Create(int tenantId, long userId, string name, string displayName)
        {
            return new Unit
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                IsActive = true
            };
        }

        public void Update(long userId, string name, string displayName)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Name = name;
            DisplayName = displayName;
        }

    }
}
