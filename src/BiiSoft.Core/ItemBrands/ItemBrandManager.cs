using System;
using BiiSoft.Items;

namespace BiiSoft.ItemBrands
{
    public class ItemBrandManager : ItemFieldManagerBase<ItemBrand>, IItemBrandManager
    {
        public ItemBrandManager(
            IBiiSoftRepository<ItemBrand, Guid> repository) : base(repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "ItemBrand";

        protected override ItemBrand CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return ItemBrand.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
