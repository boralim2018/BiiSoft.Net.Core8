using System;
using BiiSoft.Items;

namespace BiiSoft.ItemSizes
{
    public class ItemSizeManager : ItemFieldManagerBase<ItemSize>, IItemSizeManager
    {
        public ItemSizeManager(
            IBiiSoftRepository<ItemSize, Guid> repository) : base(repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "ItemSize";

        protected override ItemSize CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return ItemSize.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
