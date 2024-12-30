using Abp.Domain.Uow;
using BiiSoft.FileStorages;
using System;
using BiiSoft.Folders;
using BiiSoft.Items;

namespace BiiSoft.HDDs
{
    public class HDDManager : ItemFieldManagerBase<HDD>, IHDDManager
    {
        public HDDManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<HDD, Guid> repository) : base(appFolders, fileStorageManager, unitOfWorkManager, repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "HDD";

        protected override HDD CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return HDD.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
