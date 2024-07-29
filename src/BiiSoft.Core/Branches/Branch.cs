using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.ContactInfo;
using BiiSoft.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Branches
{

    [Table("BiiBranches")]
    public class Branch : CanModifyDefaultNameActiveEntity<Guid>, IMustHaveTenant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
        public int TenantId { get; set; }
        [MaxLength(BiiSoftConsts.MaxLengthLongCode)]
        public string BusinessId { get; protected set; }
        [MaxLength(BiiSoftConsts.MaxLengthLongCode)]
        public string PhoneNumber { get; protected set; }
        [MaxLength(BiiSoftConsts.MaxLengthLongCode)]
        public string Email { get; protected set; }
        [MaxLength(BiiSoftConsts.MaxLengthLongCode)]
        public string Website { get; protected set; }
        public string TaxRegistrationNumber { get; protected set; }

        public Guid BillingAddressId { get; set; }
        public ContactAddress BillingAddress { get; set; }
        public bool SameAsBillingAddress { get; set; }
        public Guid ShippingAddressId { get; set; }
        public ContactAddress ShippingAddress { get; set; }
      
        public static Branch Create(
            int tenantId, 
            long? userId, 
            string name, 
            string displayName, 
            string businessId, 
            string phoneNumber, 
            string email, 
            string website, 
            string taxRegistrationNumber,
            Guid billingAddressId,
            bool sameAsBillingAddress,
            Guid shippingAddressId)
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
                BillingAddressId = billingAddressId,                
                SameAsBillingAddress = sameAsBillingAddress,
                ShippingAddressId = sameAsBillingAddress ? billingAddressId : shippingAddressId,
                IsActive = true
            };
        }


        public void Update(
            long? userId,
            string name,
            string displayName,
            string businessId,
            string phoneNumber,
            string email,
            string website,
            string taxRegistrationNumber,
            Guid billingAddressId,
            bool saveAsBillingAddress,
            Guid shippingAddressId)
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
            BillingAddressId = billingAddressId;
            SameAsBillingAddress = saveAsBillingAddress;
            ShippingAddressId = SameAsBillingAddress ? billingAddressId : shippingAddressId;
            
        }
    }
}
