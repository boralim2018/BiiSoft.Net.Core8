using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Dtos
{
    public interface IIdentityDot<TPrimary>
    {
        TPrimary Id { get; set; }
    }

}
