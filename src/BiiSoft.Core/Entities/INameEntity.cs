using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Entities
{
    public interface INameEntity 
    {
        string Name { get; }
        string DisplayName { get; }
    }

    public interface INameActiveEntity: INameEntity, IActiveEntity
    {
     
    }


    public interface ICanModifyNameEntity: INameActiveEntity, ICanModifyEntity
    {

    }

    public interface IDefaultNameEntity : INameActiveEntity, IDefaultEntity
    {

    }

    public interface ICanModifyDefaultNameEntity : IDefaultNameEntity, ICanModifyEntity
    {

    }

}
