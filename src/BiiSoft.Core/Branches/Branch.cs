using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using BiiSoft.Entities;
using BiiSoft.Enums;
using BiiSoft.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BiiSoft.Branches
{

    [Table("BiiBranches")]
    public class Branch : CanModifyDefaultNameActiveEntity<Guid>, IMustHaveTenant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
        public int TenantId { get; set; }
        [MaxLength(BiiSoftConsts.MaxLengthLongCode), Required]
        public string BusinessId { get; protected set; }
        [MaxLength(BiiSoftConsts.MaxLengthLongCode)]
        public string PhoneNumber { get; protected set; }
        [MaxLength(BiiSoftConsts.MaxLengthLongCode)]
        public string Email { get; protected set; }
        [MaxLength(BiiSoftConsts.MaxLengthLongCode)]
        public string Website { get; protected set; }
        public void SetWebsite(string website) { Website = website; }

        public string TaxRegistrationNumber { get; protected set; }
        
        public static Branch Create(int tenantId, long? userId, string name, string displayName)
        {
            return new Branch
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                IsActive = true,
            };
        }

        public static Branch Create(int tenantId, long? userId, string name, string displayName, string businessId, string phoneNumber, string email, string website, string taxRegistrationNumber)
        {
            return new Branch
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                BusinessId = businessId,
                PhoneNumber = phoneNumber,
                Email = email,             
                Website = website,
                TaxRegistrationNumber = taxRegistrationNumber,
                IsActive = true
            };
        }


        public void Update(long? userId, string name, string displayName, string businessId, string phoneNumber, string email, string website, string taxRegistrationNumber)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Name = name;
            DisplayName = displayName;
            BusinessId = businessId;
            PhoneNumber = phoneNumber;
            Email = email;
            Website = website;
            TaxRegistrationNumber = taxRegistrationNumber;
        }
    }
}
