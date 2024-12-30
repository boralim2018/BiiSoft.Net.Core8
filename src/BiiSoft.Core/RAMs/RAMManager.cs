using Abp.Domain.Uow;
using BiiSoft.FileStorages;
using System;
using BiiSoft.Folders;
using BiiSoft.Items;

namespace BiiSoft.RAMs
{
    public class RAMManager : ItemFieldManagerBase<RAM>, IRAMManager
    {
        public RAMManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<RAM, Guid> repository) : base(appFolders, fileStorageManager, unitOfWorkManager, repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "RAM";

        protected override RAM CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return RAM.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
