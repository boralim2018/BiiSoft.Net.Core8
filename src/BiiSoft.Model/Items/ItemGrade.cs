using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Timing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BiiSoft.Entities;

namespace BiiSoft.Items
{
    [Table("BiiItemGrades")]
    public class ItemGrade : DefaultNameActiveEntity<Guid>, IMustHaveTenant, INoEntity
    {
        public int TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }

        public static ItemGrade Create(int tenantId, long userId, string name, string displayNmae)
        {
            return new ItemGrade
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayNmae,
                IsActive = true
            };
        }

        public void Update(long userId, string name, string displayNmae)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Name = name;
            DisplayName = displayNmae;
        }

    }
}
