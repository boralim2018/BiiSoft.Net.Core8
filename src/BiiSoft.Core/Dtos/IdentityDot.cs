using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Dtos
{
    public class IdentityDot<TPrimary> : IIdentityDot<TPrimary>
    {
        public TPrimary Id { get; set; }
    }
}
