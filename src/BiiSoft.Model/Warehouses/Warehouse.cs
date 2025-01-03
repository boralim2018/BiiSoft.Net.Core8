using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Entities;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiiSoft.Warehouses
{

    [Table("BiiWarehouses")]
    public class Warehouse : DefaultNameActiveEntity<Guid>, IMayHaveTenant, INoEntity, ICodeEntity
    {
        public int? TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }

        [MaxLength(BiiSoftConsts.MaxLengthItemFieldCode)]
        [StringLength(BiiSoftConsts.MaxLengthItemFieldCode, ErrorMessage = BiiSoftConsts.MaxLengthItemFieldCodeErrorMessage)]
        public string Code { get; protected set; }
        public void SetCode(string code) => Code = code;
        public BranchSharing Sharing { get; private set; }
        public void SetSharing(BranchSharing sharing) => Sharing = sharing;

        public ICollection<WarehouseBranch> WarehouseBranches { get; set; }

        public static Warehouse Create(int tenantId, long userId, string name, string displayName, string code, BranchSharing sharing)
        {
            return new Warehouse
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                Code = code,
                Sharing = sharing,
                IsActive = true
            };
        }

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
