using Abp.Domain.Entities;
using BiiSoft.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.ContactInfo
{
    public class ChangeContactAddressLocationInput: UserEntity<Guid>
    {
        public Guid? LocationId { get; set; }
    }
}
