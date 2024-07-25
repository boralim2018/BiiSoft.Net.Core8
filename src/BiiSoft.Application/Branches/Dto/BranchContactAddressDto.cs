using BiiSoft.ContactInfo.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Branches.Dto
{
    public class BranchContactAddressDto : ContactAddressDto
    {
        public bool IsDefault { get; set; }
    }
}
