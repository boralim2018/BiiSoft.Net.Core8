using Abp.Domain.Entities.Auditing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Entities
{
    [Serializable]
    public abstract class CanModifyEntity<TPrimary> : BaseAuditedEntity<TPrimary>, ICanModifyEntity
    {
        [Required]
        public bool CannotEdit { get; protected set; }
        [Required]
        public bool CannotDelete { get; protected set; }
        public void SetCannotEdit(bool cannotEdit) => CannotEdit = cannotEdit;
        public void SetCannotDelete(bool cannotDelete) => CannotDelete = cannotDelete;
    }
}
