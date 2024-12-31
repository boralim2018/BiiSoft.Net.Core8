using System;
using BiiSoft.Items;

namespace BiiSoft.ItemModels
{
    public class ItemModelManager : ItemFieldManagerBase<ItemModel>, IItemModelManager
    {
        public ItemModelManager(
            IBiiSoftRepository<ItemModel, Guid> repository) : base(repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "ItemModel";

        protected override ItemModel CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return ItemModel.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
