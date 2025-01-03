﻿using BiiSoft.ContactInfo.Dto;
using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;

namespace BiiSoft.Branches.Dto
{
    public class BranchDetailDto : DefaultNameActiveAuditedNavigationDto<Guid>, INoDto
    {
        public long No { get; set; }
        public string BusinessId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string TaxRegistrationNumber { get; set; }

        public ContactAddressDto BillingAddress { get; set; }
        public bool SameAsBillingAddress { get; set; }
        public ContactAddressDto ShippingAddress { get; set; }
        public Sharing Sharing { get; set; }
    }
}
