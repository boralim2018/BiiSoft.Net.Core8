using Abp.Domain.Entities;
using BiiSoft.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BiiSoft.ContactInfo
{
        
    public abstract class ContactPersonBase<TPrimaryKey> : NameActiveEntity<TPrimaryKey>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [MaxLength(BiiSoftConsts.MaxLengthName)]
        public string Title { get; protected set; }
        [MaxLength(BiiSoftConsts.MaxLengthName)]
        public string Surname { get; protected set; }
        [MaxLength(BiiSoftConsts.MaxLengthName)]
        public string DisplaySurname { get; protected set; }
        [MaxLength(BiiSoftConsts.MaxLengthName)]
        public string PhoneNumber { get; protected set; }
        [MaxLength(BiiSoftConsts.MaxLengthName)]
        public string Email { get; protected set; }
        public bool DisplayNameFirst { get; protected set; }


    }

    public abstract class ContactPersonBase : ContactPersonBase<Guid>
    {

    }
}
