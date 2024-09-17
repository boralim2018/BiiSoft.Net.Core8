using Abp.Domain.Entities;
using Abp.Timing;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using BiiSoft.Taxes;
using BiiSoft.Entities;

namespace BiiSoft.ChartOfAccounts
{
    [Table("BiiChartOfAccounts")]
    public class ChartOfAccount: CanModifyNameActiveEntity<Guid>, IMayHaveTenant, INoEntity
    {
        public int? TenantId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long No { get; private set; }
        [Required]
        [MaxLength(BiiSoftConsts.MaxLengthCode)]
        [StringLength(BiiSoftConsts.MaxLengthCode, ErrorMessage = BiiSoftConsts.MaxLengthCodeErrorMessage)]
        public string Code { get; private set; }

        public AccountType AccountType { get; private set; }
        public SubAccountType SubAccountType { get; private set; }
        public Guid? ParentId { get; private set; }
        public  ChartOfAccount Parent { get; private set; }
        public void SetParent(Guid? parentId) => ParentId = parentId;
        public void SetParent(ChartOfAccount parent) => Parent = parent;

        public Guid? PurchaseTaxId { get; private set; }
        public Tax PurchaseTax { get; private set; }
        public Guid? SaleTaxId { get; private set; }
        public Tax SaleTax { get; private set; }


        public static ChartOfAccount Create(int? tenantId, long userId, SubAccountType subAccountType, string code, string name, string displayName)
        {
            return new ChartOfAccount
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                AccountType = subAccountType.Parent(),
                SubAccountType = subAccountType,
                Code = code,
                Name = name,
                DisplayName = displayName,
                IsActive = true
            };
        }

        public void Update(long userId, SubAccountType subAccountType, string code, string name, string displayName)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            AccountType = subAccountType.Parent();
            SubAccountType = subAccountType;
            Code = code;
            Name = name;
            DisplayName = displayName;
        }

    }
}
