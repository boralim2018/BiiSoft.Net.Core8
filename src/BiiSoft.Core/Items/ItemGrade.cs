using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Timing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BiiSoft.Extensions;
using BiiSoft.Entities;

namespace BiiSoft.Items
{
    [Table("BiiItemGrades")]
    public class ItemGrade : DefaultNameActiveEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [MaxLength(BiiSoftConsts.MaxLengthName)]
        [StringLength(BiiSoftConsts.MaxLengthName, ErrorMessage = BiiSoftConsts.MaxLengthNameErrorMessage)]
        public string Description { get; private set; }

        public static ItemGrade Create(int tenantId, long userId, string name, string displayNmae, string description)
        {
            return new ItemGrade
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayNmae,
                Description = description,
                IsActive = true
            };
        }

        public void Update(long userId, string name, string displayNmae, string description)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Name = name;
            DisplayName = displayNmae;
            Description = description;
        }

    }
}
