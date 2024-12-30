using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using BiiSoft.Entities;

namespace BiiSoft.Items
{
    public abstract class ItemFieldBase : DefaultNameActiveEntity<Guid>, IMayHaveTenant, INoEntity, ICodeEntity
    {
        public int? TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }

        [MaxLength(BiiSoftConsts.MaxLengthItemFieldCode)]
        [StringLength(BiiSoftConsts.MaxLengthItemFieldCode, ErrorMessage = BiiSoftConsts.MaxLengthItemFieldCodeErrorMessage)]
        public string Code { get; protected set; }
        public void SetCode(string code) => Code = code;

        public void Update(long userId, string name, string displayName, string code)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Name = name;
            DisplayName = displayName;
            Code = code;
        }
    }
}
