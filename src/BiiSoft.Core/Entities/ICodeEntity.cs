using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Entities
{
    public interface ICodeEntity
    {
        string Code { get; }
        void SetCode(string code);
    }

}
