using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BiiSoft.Entities
{
    [Serializable]
    public abstract class ActiveEntity<TPrimary> : AuditedEntity<TPrimary>, IActiveEntity
    {
        [Required]
        public bool IsActive { get; protected set; }
        public void Enable(bool isActive) => IsActive = isActive;
    }
}
