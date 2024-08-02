using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BiiSoft.Classes
{
    public class Class : DefaultNameActiveEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

       
        [Required, MaxLength(BiiSoftConsts.MaxLengthCode)]
        public string Code { get; protected set; }

        public static Class Create (int tenantId, long? userId, string name, string displayName, string code)
        {
            return new Class
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Code = code,
                IsActive = true,
            };
        }

       
        public void Update(long? userId, string name, string displayName, string code)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Name = name;
            DisplayName = displayName;
            Code = code;
        }
    }
}
