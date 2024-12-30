using Abp.Domain.Uow;
using BiiSoft.FileStorages;
using System;
using BiiSoft.Folders;

namespace BiiSoft.Items.Series
{
    public class ItemSeriesManager : ItemFieldManagerBase<ItemSeries>, IItemSeriesManager
    {
        public ItemSeriesManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<ItemSeries, Guid> repository) : base(appFolders, fileStorageManager, unitOfWorkManager, repository)
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
