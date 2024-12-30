using Abp.Domain.Uow;
using BiiSoft.FileStorages;
using System;
using BiiSoft.Folders;
using BiiSoft.Items;

namespace BiiSoft.FieldBs
{
    public class FieldBManager : ItemFieldManagerBase<FieldB>, IFieldBManager
    {
        public FieldBManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<FieldB, Guid> repository) : base(appFolders, fileStorageManager, unitOfWorkManager, repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "FieldB";

        protected override FieldB CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return FieldB.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
