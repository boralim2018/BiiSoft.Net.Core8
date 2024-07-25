using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<BranchContactAddressDto> ContactAddresses { get; set; }
    }

}
