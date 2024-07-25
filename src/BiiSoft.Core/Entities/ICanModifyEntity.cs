using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Entities
{
    public interface ICanModifyEntity
    {
        public bool CannotEdit { get; }
        public bool CannotDelete { get; }
        void SetCannotEdit(bool cannotEdit);
        void SetCannotDelete(bool cannotDelete);
    }
}
