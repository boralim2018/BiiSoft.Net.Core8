using BiiSoft.Dtos;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;

namespace BiiSoft.Branches.Dto
{
    public class BranchListDto : DefaultNameActiveAuditedDto<Guid>
    {
        public long No { get; set; }
        public string BusinessId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string TaxRegistrationNumber { get; set; }
        public Sharing Sharing { get; set; }
        public string SharingName { get; set; }
    }
}
