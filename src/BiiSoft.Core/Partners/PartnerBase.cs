using BiiSoft.ContactInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BiiSoft.Entities;

namespace BiiSoft.Partners
{
    public abstract class PartnerBase<TPrimaryKey> : ContactPersonBase<TPrimaryKey>
    {
        [MaxLength(BiiSoftConsts.MaxLengthCode), Required]
        public string Code { get; protected set; } 
       
        [MaxLength(BiiSoftConsts.MaxLengthName)]
        public string Website { get; protected set; }
  
    }

    public abstract class PartnerBase : PartnerBase<Guid>
    {

    }
}
