using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Entities
{
    public interface IActiveEntity
    {
        bool IsActive { get; }
        void Enable(bool isActive);
    }
}
