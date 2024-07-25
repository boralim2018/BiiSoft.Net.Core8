using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BiiSoft.Branches.Dto
{
    public class BranchDetailDto : DefaultNameActiveAuditedDto<Guid>
    {
        public long No { get; set; }
        public string BusinessId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string TaxRegistrationNumber { get; set; }

        public Guid? FirstId { get; set; }
        public Guid? NextId { get; set; }
        public Guid? PreviousId { get; set; }
        public Guid? LastId { get; set; }
        public List<BranchContactAddressDto> ContactAddresses { get; set; }
    }
}
