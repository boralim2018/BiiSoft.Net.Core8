using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Entities
{
    [Serializable]
    public abstract class NameEntity<TPrimary> : AuditedEntity<TPrimary>, INameEntity
    {
        [Required]
        [MaxLength(BiiSoftConsts.MaxLengthName)]
        [StringLength(BiiSoftConsts.MaxLengthName, ErrorMessage = BiiSoftConsts.MaxLengthNameErrorMessage)]
        public string Name { get; protected set; }
        [Required]
        [MaxLength(BiiSoftConsts.MaxLengthLongName)]
        [StringLength(BiiSoftConsts.MaxLengthLongName, ErrorMessage = BiiSoftConsts.MaxLengthLongNameErrorMessage)]
        public string DisplayName { get; protected set; }
    }

    [Serializable]
    public abstract class NameActiveEntity<TPrimary> : NameEntity<TPrimary>, INameActiveEntity
    {   
        [Required]
        public bool IsActive { get; protected set; }
        public void Enable(bool isActive) => IsActive = isActive;
    }


    [Serializable]
    public abstract class CanModifyNameActiveEntity<TPrimary> : NameActiveEntity<TPrimary>, ICanModifyNameEntity
    {
        public bool CannotEdit { get; protected set; }
        public bool CannotDelete { get; protected set; }
        public void SetCannotDelete(bool cannotDelete) => CannotDelete = cannotDelete;
        public void SetCannotEdit(bool cannotEdit) => CannotEdit = cannotEdit;
    }


    [Serializable]
    public abstract class DefaultNameActiveEntity<TPrimary> : NameActiveEntity<TPrimary>, IDefaultNameEntity
    {
        public bool IsDefault { get; protected set; }
        public void SetDefault(bool isDefault) => IsDefault = isDefault; 
    }


    [Serializable]
    public abstract class CanModifyDefaultNameActiveEntity<TPrimary> : DefaultNameActiveEntity<TPrimary>, ICanModifyDefaultNameEntity
    {
        public bool CannotEdit { get; protected set; }
        public bool CannotDelete { get; protected set; }
        public void SetCannotDelete(bool cannotDelete) => CannotDelete = cannotDelete;
        public void SetCannotEdit(bool cannotEdit) => CannotEdit = cannotEdit;
    }

}
