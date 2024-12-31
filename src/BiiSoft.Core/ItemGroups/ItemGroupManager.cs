using System;
using BiiSoft.Items;

namespace BiiSoft.ItemGroups
{
    public class ItemGroupManager : ItemFieldManagerBase<ItemGroup>, IItemGroupManager
    {
        public ItemGroupManager(
            IBiiSoftRepository<ItemGroup, Guid> repository) : base(repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "ItemGroup";

        protected override ItemGroup CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return ItemGroup.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
