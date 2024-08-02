using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Entities
{
    public interface IDescriptionEntity
    {
        string Description { get; }
    }

    public interface IDescriptionNameEntity: INameActiveEntity, IDescriptionEntity
    {

    }

    public interface ICanModifyDescriptionNameEntity: ICanModifyNameEntity, IDescriptionEntity
    {

    }

    public interface IDefaultDescriptionNameEntity : IDefaultNameEntity, IDescriptionEntity
    {

    }

    public interface ICanModifyDefaultDescriptionNameEntity : ICanModifyDefaultNameEntity, IDescriptionEntity
    {

    }

    public interface INoteEntity
    {
        string Note { get; }
    }
}
