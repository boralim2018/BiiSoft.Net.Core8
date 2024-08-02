using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Entities
{
    [Serializable]
    public abstract class DefaultEntity<TPrimary>: AuditedEntity<TPrimary>, IDefaultEntity
    {
        public bool IsDefault { get; protected set; }
        public void SetDefault(bool isDefault) => IsDefault = isDefault;
    }

    [Serializable]
    public abstract class DefaultActiveEntity<TPrimary> : ActiveEntity<TPrimary>, IDefaultActiveEntity
    {
        public bool IsDefault { get; protected set; }
        public void SetDefault(bool isDefault) => IsDefault = isDefault;
    }
}
