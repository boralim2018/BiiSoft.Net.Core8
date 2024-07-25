using Abp.Timing;
using BiiSoft.ChartOfAccounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BiiSoft.Enums;

namespace BiiSoft.Partners
{
    public enum PartnerType
    {
        Vendor = 1,
        Customer = 2,
        Employee = 3,        
    }

    public enum BusinessProfile
    {
        Personal = 1,
        Company = 2,
    }

    public class Partner : PartnerBase
    {

        public PartnerType PartnerType { get; private set; }
        public BusinessProfile BusinessProfile { get; private set; }

        private bool _isShowInVendor;
        public bool IsShowInVendor
        {
            get => PartnerType == PartnerType.Vendor || _isShowInVendor;
            protected set => _isShowInVendor = value;
        }

        public void ShowInVendor() { this.IsShowInVendor = true; }
        public void UnShowInVendor() { this.IsShowInVendor = false; }

        private bool _isShowInCustomer;
        public bool IsShowInCustomer
        {
            get => PartnerType == PartnerType.Customer || _isShowInCustomer;
            protected set => _isShowInCustomer = value;
        }
        public void ShowInCustomer() { this.IsShowInCustomer = true; }
        public void UnShowInCustomer() { this.IsShowInCustomer = false; }

        public Guid? EmployeeId { get; protected set; }
        public Employee Employee { get; protected set; }


        public long? PartnerGroupId { get; protected set; }
        public PartnerGroup PartnerGroup { get; protected set; }
        public void SetPartnerGroup(long? partnerGroupId) { this.PartnerGroupId = partnerGroupId; }


        public bool IsSameAsBillingAddress { get; protected set; } 

        public SharingType SharingType { get; protected set; }
        public void SetAsPublicSharing() { this.SharingType = SharingType.Public; }
        public void SetAsUserSharing() { this.SharingType = SharingType.User; }

        public Guid? ARAccountId { get; protected set; }
        public ChartOfAccount ARAccount { get; protected set; }
        public void SetARAccount(Guid? arAccountId) { this.ARAccountId = arAccountId; }

        public Guid? APAccountId { get; protected set; }
        public ChartOfAccount APAccount { get; protected set; }
        public void SetAPAccount(Guid? apAccountId) { this.APAccountId = apAccountId; }

        public string TaxRegistrationNumber { get; protected set; } 

        public static Partner Create
            (
            int tenantId,
            long? userId,
            PartnerType type,
            BusinessProfile businessProfile,
            string code,
            string name,
            string displayName,
            string phoneNumber,
            string email,
            string website,
            bool isSameAsBillingAddress,
            string taxRegistrationNumber
            )
        {
            return new Partner
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                PartnerType = type,
                BusinessProfile = businessProfile,
                Code = code,
                Name = name,
                DisplayName = displayName,
                PhoneNumber = phoneNumber,
                Email = email,
                Website = website,
                IsSameAsBillingAddress = isSameAsBillingAddress,
                TaxRegistrationNumber = taxRegistrationNumber,
                IsActive = true,
            };
        }

        public void Update
            (
            long? userId,
            PartnerType type,
            BusinessProfile businessProfile,
            string code,
            string name,
            string displayName,
            string phoneNumber,
            string email,
            string website,
            bool isSameAsBillingAddress,
            string taxRegistrationNumber
            )
        {
            CreatorUserId = userId;
            CreationTime = Clock.Now;
            PartnerType = type;
            BusinessProfile = businessProfile;
            Code = code;
            Name = name;
            DisplayName = displayName;
            PhoneNumber = phoneNumber;
            Email = email;
            Website = website;
            IsSameAsBillingAddress = isSameAsBillingAddress;
            TaxRegistrationNumber = taxRegistrationNumber;
        }
    }
}
