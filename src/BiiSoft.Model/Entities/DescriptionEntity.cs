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
    public abstract class DescriptionEntity<TPrimary> : AuditedEntity<TPrimary>, IDescriptionEntity
    {

        [MaxLength(BiiSoftConsts.MaxLengthDescription)]
        [StringLength(BiiSoftConsts.MaxLengthDescription, ErrorMessage = BiiSoftConsts.MaxLengthDescriptionErrorMessage)]
        public string Description { get; protected set; }
    }

    [Serializable]
    public abstract class LongDescriptionEntity<TPrimary> : AuditedEntity<TPrimary>, IDescriptionEntity
    {

        [MaxLength(BiiSoftConsts.MaxLengthLongDescription)]
        [StringLength(BiiSoftConsts.MaxLengthLongDescription, ErrorMessage = BiiSoftConsts.MaxLengthLongDescriptionErrorMessage)]
        public string Description { get; protected set; }
    }


    [Serializable]
    public abstract class DescriptionNameEntity<TPrimary> : NameActiveEntity<TPrimary>, IDescriptionNameEntity
    {

        [MaxLength(BiiSoftConsts.MaxLengthDescription)]
        [StringLength(BiiSoftConsts.MaxLengthDescription, ErrorMessage = BiiSoftConsts.MaxLengthDescriptionErrorMessage)]
        public string Description { get; protected set; }
    }

    [Serializable]
    public abstract class LongDescriptionNameEntity<TPrimary> : NameActiveEntity<TPrimary>, IDescriptionNameEntity
    {

        [MaxLength(BiiSoftConsts.MaxLengthLongDescription)]
        [StringLength(BiiSoftConsts.MaxLengthLongDescription, ErrorMessage = BiiSoftConsts.MaxLengthLongDescriptionErrorMessage)]
        public string Description { get; protected set; }
    }


    [Serializable]
    public abstract class CanModifyDescriptionNameEntity<TPrimary> : CanModifyNameActiveEntity<TPrimary>, ICanModifyDescriptionNameEntity
    {
        [MaxLength(BiiSoftConsts.MaxLengthDescription)]
        [StringLength(BiiSoftConsts.MaxLengthDescription, ErrorMessage = BiiSoftConsts.MaxLengthDescriptionErrorMessage)]
        public string Description { get; protected set; }
    }

    [Serializable]
    public abstract class CanModifyLongDescriptionNameEntity<TPrimary> : CanModifyNameActiveEntity<TPrimary>, ICanModifyDescriptionNameEntity
    {

        [MaxLength(BiiSoftConsts.MaxLengthLongDescription)]
        [StringLength(BiiSoftConsts.MaxLengthLongDescription, ErrorMessage = BiiSoftConsts.MaxLengthLongDescriptionErrorMessage)]
        public string Description { get; protected set; }
    }


    [Serializable]
    public abstract class DefaultDescriptionNameEntity<TPrimary> : DefaultNameActiveEntity<TPrimary>, IDefaultDescriptionNameEntity
    {
        [MaxLength(BiiSoftConsts.MaxLengthDescription)]
        [StringLength(BiiSoftConsts.MaxLengthDescription, ErrorMessage = BiiSoftConsts.MaxLengthDescriptionErrorMessage)]
        public string Description { get; protected set; }
    }

    [Serializable]
    public abstract class DefaultLongDescriptionNameEntity<TPrimary> : DefaultNameActiveEntity<TPrimary>, IDefaultDescriptionNameEntity
    {

        [MaxLength(BiiSoftConsts.MaxLengthLongDescription)]
        [StringLength(BiiSoftConsts.MaxLengthLongDescription, ErrorMessage = BiiSoftConsts.MaxLengthLongDescriptionErrorMessage)]
        public string Description { get; protected set; }
    }

    [Serializable]
    public abstract class CanModifyDefaultDescriptionNameEntity<TPrimary> : CanModifyDefaultNameActiveEntity<TPrimary>, ICanModifyDefaultDescriptionNameEntity
    {
        [MaxLength(BiiSoftConsts.MaxLengthDescription)]
        [StringLength(BiiSoftConsts.MaxLengthDescription, ErrorMessage = BiiSoftConsts.MaxLengthDescriptionErrorMessage)]
        public string Description { get; protected set; }
    }

    [Serializable]
    public abstract class CanModifyDefaultLongDescriptionNameEntity<TPrimary> : CanModifyDefaultNameActiveEntity<TPrimary>, ICanModifyDefaultDescriptionNameEntity
    {

        [MaxLength(BiiSoftConsts.MaxLengthLongDescription)]
        [StringLength(BiiSoftConsts.MaxLengthLongDescription, ErrorMessage = BiiSoftConsts.MaxLengthLongDescriptionErrorMessage)]
        public string Description { get; protected set; }
    }


    [Serializable]
    public abstract class NoteEntity<TPrimary> : AuditedEntity<TPrimary>, INoteEntity
    {

        [MaxLength(BiiSoftConsts.MaxLengthNote)]
        [StringLength(BiiSoftConsts.MaxLengthNote, ErrorMessage = BiiSoftConsts.MaxLengthNoteErrorMessage)]
        public string Note { get; protected set; }
    }

    [Serializable]
    public abstract class LongNoteEntity<TPrimary> : AuditedEntity<TPrimary>, INoteEntity
    {

        [MaxLength(BiiSoftConsts.MaxLengthLongNote)]
        [StringLength(BiiSoftConsts.MaxLengthLongNote, ErrorMessage = BiiSoftConsts.MaxLengthLongNoteErrorMessage)]
        public string Note { get; protected set; }
    }

}
