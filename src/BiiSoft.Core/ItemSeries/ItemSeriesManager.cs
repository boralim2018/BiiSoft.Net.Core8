using System;

namespace BiiSoft.Items.Series
{
    public class ItemSeriesManager : ItemFieldManagerBase<ItemSeries>, IItemSeriesManager
    {
        public ItemSeriesManager(
            IBiiSoftRepository<ItemSeries, Guid> repository) : base(repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "ItemSeries";

        protected override ItemSeries CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return ItemSeries.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
