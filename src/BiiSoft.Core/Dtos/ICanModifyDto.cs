using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Dtos
{
    public interface ICanModifyDto
    {
        bool CannotEdit { get; set; }
        bool CannotDelete { get; set; }
    }
}
