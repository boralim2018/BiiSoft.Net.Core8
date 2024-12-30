using Abp.Domain.Uow;
using BiiSoft.FileStorages;
using System;
using BiiSoft.Folders;
using BiiSoft.Items;

namespace BiiSoft.Screens
{
    public class ScreenManager : ItemFieldManagerBase<Screen>, IScreenManager
    {
        public ScreenManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<Screen, Guid> repository) : base(appFolders, fileStorageManager, unitOfWorkManager, repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "Screen";

        protected override Screen CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return Screen.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
