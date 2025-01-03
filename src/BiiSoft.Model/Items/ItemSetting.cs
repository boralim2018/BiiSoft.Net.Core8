using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Items
{
    [Table("BiiItemSettings")]
    public class ItemSetting : ActiveEntity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public bool CodeFormalaEnable { get; private set; }
       
        public static ItemSetting Create(int tenantId, long userId, bool codeFormulaEnble)
        {
            return new ItemSetting
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                CodeFormalaEnable = codeFormulaEnble,
            };
        }

        public void Update(long userId, bool codeFormulaEnble)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            CodeFormalaEnable = codeFormulaEnble;
        }
    }
}
