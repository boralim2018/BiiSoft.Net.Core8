﻿using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using BiiSoft.Extensions;
using BiiSoft.Entities;

namespace BiiSoft.Items
{
    [Table("BiiColorPatterns")]
    public class ColorPattern : CanModifyDefaultNameActiveEntity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public static ColorPattern Create(int? tenantId, long userId, string name, string displayName)
        {
            return new ColorPattern
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
