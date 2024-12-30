using Abp.Domain.Uow;
using BiiSoft.FileStorages;
using System;
using BiiSoft.Folders;
using BiiSoft.Items;

namespace BiiSoft.ItemGroups
{
    public class ItemGroupManager : ItemFieldManagerBase<ItemGroup>, IItemGroupManager
    {
        public ItemGroupManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<ItemGroup, Guid> repository) : base(appFolders, fileStorageManager, unitOfWorkManager, repository)
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
