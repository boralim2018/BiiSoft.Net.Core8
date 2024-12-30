using Abp.Domain.Uow;
using BiiSoft.FileStorages;
using System;
using BiiSoft.Folders;
using BiiSoft.Items;

namespace BiiSoft.FieldAs
{
    public class FieldAManager : ItemFieldManagerBase<FieldA>, IFieldAManager
    {
        public FieldAManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<FieldA, Guid> repository) : base(appFolders, fileStorageManager, unitOfWorkManager, repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "FieldA";

        protected override FieldA CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return FieldA.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
