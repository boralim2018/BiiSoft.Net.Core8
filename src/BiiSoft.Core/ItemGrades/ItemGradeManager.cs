using System;
using BiiSoft.Items;

namespace BiiSoft.ItemGrades
{
    public class ItemGradeManager : ItemFieldManagerBase<ItemGrade>, IItemGradeManager
    {
        public ItemGradeManager(
            IBiiSoftRepository<ItemGrade, Guid> repository) : base(repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "ItemGrade";

        protected override ItemGrade CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return ItemGrade.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
