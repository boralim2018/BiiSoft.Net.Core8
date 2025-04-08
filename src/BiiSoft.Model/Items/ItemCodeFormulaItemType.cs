using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Entities;
using BiiSoft.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Items
{

    [Table("BiiItemCodeFormulaItemTypes")]
    public class ItemCodeFormulaItemType : BaseAuditedEntity<Guid>, IMustHaveTenant, INoEntity
    {
        public int TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
        public ItemType ItemType { get; private set; }
        public Guid ItemCodeFormulaId { get; private set; }
        public ItemCodeFormula ItemCodeFormula { get; private set; }

        public static ItemCodeFormulaItemType Create(int tenantId, long userId, ItemType itemType, Guid itemCodeFormulaId)
        {
            return new ItemCodeFormulaItemType
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                ItemType = itemType,
                ItemCodeFormulaId = itemCodeFormulaId
            };
        }

        public void Update(long userId, ItemType itemType)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            ItemType = itemType;
        }
       
    }
}
