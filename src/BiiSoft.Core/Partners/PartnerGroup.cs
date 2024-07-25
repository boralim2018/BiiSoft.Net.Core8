using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Entities;
using BiiSoft.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BiiSoft.Partners
{
    public class PartnerGroup : NameActiveEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public PartnerType PartnerType { get; protected set; }


        public static PartnerGroup Create(int tenantId, long? userId, string name, string displayName, PartnerType type)
        {
            return new PartnerGroup
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName, 
                PartnerType = type,
                IsActive = true,
            };
        }

        public PartnerGroup Update(long? userId, string name, string displayName, PartnerType type)
        {   
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Name = name;
            DisplayName = displayName;
            PartnerType = type;

            return this;
        }
    }
}
