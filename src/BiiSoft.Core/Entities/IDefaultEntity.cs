using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Entities
{
    public interface IDefaultEntity
    {
        bool IsDefault { get; }
        void SetDefault(bool isDefault);
    }

    public interface IDefaultActiveEntity: IDefaultEntity, IActiveEntity
    {

    }
}
