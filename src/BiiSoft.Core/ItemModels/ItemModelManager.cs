using Abp.Domain.Uow;
using BiiSoft.FileStorages;
using System;
using BiiSoft.Folders;
using BiiSoft.Items;

namespace BiiSoft.ItemModels
{
    public class ItemModelManager : ItemFieldManagerBase<ItemModel>, IItemModelManager
    {
        public ItemModelManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<ItemModel, Guid> repository) : base(appFolders, fileStorageManager, unitOfWorkManager, repository)
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
