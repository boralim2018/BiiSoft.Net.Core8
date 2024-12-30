using Abp.Domain.Uow;
using BiiSoft.FileStorages;
using System;
using BiiSoft.Folders;
using BiiSoft.Items;

namespace BiiSoft.Batteries
{
    public class BatteryManager : ItemFieldManagerBase<Battery>, IBatteryManager
    {
        public BatteryManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<Battery, Guid> repository) : base(appFolders, fileStorageManager, unitOfWorkManager, repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "Battery";

        protected override Battery CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return Battery.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
