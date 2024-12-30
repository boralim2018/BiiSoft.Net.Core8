using Abp.Domain.Uow;
using BiiSoft.FileStorages;
using System;
using BiiSoft.Folders;
using BiiSoft.Items;

namespace BiiSoft.ItemBrands
{
    public class ItemBrandManager : ItemFieldManagerBase<ItemBrand>, IItemBrandManager
    {
        public ItemBrandManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<ItemBrand, Guid> repository) : base(appFolders, fileStorageManager, unitOfWorkManager, repository)
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
