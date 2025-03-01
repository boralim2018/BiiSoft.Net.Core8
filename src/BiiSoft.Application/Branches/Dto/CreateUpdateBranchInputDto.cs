﻿using BiiSoft.ContactInfo.Dto;
using System;

namespace BiiSoft.Branches.Dto
{
    public class CreateUpdateBranchInputDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string BusinessId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string TaxRegistrationNumber { get; set; }
        public ContactAddressDto BillingAddress { get; set; }
        public bool SameAsBillingAddress { get; set; }
        public ContactAddressDto ShippingAddress { get; set; }
    }

}
