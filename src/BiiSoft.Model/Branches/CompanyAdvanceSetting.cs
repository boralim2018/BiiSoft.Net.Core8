using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using BiiSoft.Enums;
using System.ComponentModel;

namespace BiiSoft.Branches
{

    [Table("BiiCompanyAdvanceSettings")]
    public class CompanyAdvanceSetting : AuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public bool MultiBranchesEnable { get; protected set; }
        public void EnableMultiBranches(bool enable) => MultiBranchesEnable = enable;
        public bool MultiCurrencyEnable { get; protected set; }
        public void EnableMultiCurrency(bool enable) => MultiCurrencyEnable = enable;
        public bool LineDiscountEnable { get; protected set; }
        public void EnableLineDiscount(bool enable) => LineDiscountEnable = enable;
        public bool TotalDiscountEnable { get; protected set; }
        public void EnableTotalDiscount(bool enable) => TotalDiscountEnable = enable;
        public bool CustomTransactionNoEnable { get; set; }
        public void EnableCustomTransactionNo(bool enable) => CustomTransactionNoEnable = enable;
        public bool ClassEnable { get; protected set; }
        public void EnableClass(bool enable) => ClassEnable = enable;

        public AddressLevel ContactAddressLevel { get; protected set; }
        public void SetAddressLevel(AddressLevel addressLevel) => ContactAddressLevel = addressLevel;


        public static CompanyAdvanceSetting Create(
            int tenantId,
            long? userId,
            bool multiBranchesEnable,
            bool multiCurrencyEnable,
            bool lineDiscountEnable,
            bool totalDiscountEnable,
            bool customTransactionNoEnable,
            bool classEnable,
            AddressLevel contactAddressLevel)
        {
            return new CompanyAdvanceSetting
            {
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                MultiBranchesEnable = multiBranchesEnable,
                MultiCurrencyEnable = multiCurrencyEnable,
                LineDiscountEnable = lineDiscountEnable,
                TotalDiscountEnable = totalDiscountEnable,
                CustomTransactionNoEnable = customTransactionNoEnable,
                ClassEnable = classEnable,
                ContactAddressLevel = contactAddressLevel
            };
        }

        public void Update(
            long? userId,
            bool multiBranchesEnable,
            bool multiCurrencyEnable,
            bool lineDiscountEnable,
            bool totalDiscountEnable,
            bool customTransactionNoEnable,
            bool classEnable,
            AddressLevel contactAddressLevel)
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            MultiBranchesEnable = multiBranchesEnable;
            MultiCurrencyEnable = multiCurrencyEnable;
            LineDiscountEnable = lineDiscountEnable;
            TotalDiscountEnable = totalDiscountEnable;
            CustomTransactionNoEnable = customTransactionNoEnable;
            ClassEnable = classEnable;
            ContactAddressLevel = contactAddressLevel;
        }
    }
}
