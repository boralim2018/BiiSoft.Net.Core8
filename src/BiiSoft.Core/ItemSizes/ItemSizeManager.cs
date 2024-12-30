using Abp.Domain.Uow;
using BiiSoft.FileStorages;
using System;
using BiiSoft.Folders;
using BiiSoft.Items;

namespace BiiSoft.ItemSizes
{
    public class ItemSizeManager : ItemFieldManagerBase<ItemSize>, IItemSizeManager
    {
        public ItemSizeManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<ItemSize, Guid> repository) : base(appFolders, fileStorageManager, unitOfWorkManager, repository)
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
