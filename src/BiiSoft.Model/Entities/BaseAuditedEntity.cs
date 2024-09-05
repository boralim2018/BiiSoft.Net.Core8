using Abp.Domain.Entities.Auditing;
using BiiSoft.Authorization.Users;

namespace BiiSoft.Entities
{
    public abstract class BaseAuditedEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>
    {        
        public virtual User LastModifierUser { get; protected set; }
        public virtual User CreatorUser { get; protected set; }
    }
}
