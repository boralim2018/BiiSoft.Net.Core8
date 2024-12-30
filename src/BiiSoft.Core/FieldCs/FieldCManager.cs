using Abp.Domain.Uow;
using BiiSoft.FileStorages;
using System;
using BiiSoft.Folders;
using BiiSoft.Items;

namespace BiiSoft.FieldCs
{
    public class FieldCManager : ItemFieldManagerBase<FieldC>, IFieldCManager
    {
        public FieldCManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<FieldC, Guid> repository) : base(appFolders, fileStorageManager, unitOfWorkManager, repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "FieldC";

        protected override FieldC CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return FieldC.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
