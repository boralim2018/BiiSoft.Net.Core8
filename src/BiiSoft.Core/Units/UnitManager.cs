using Abp.Domain.Uow;
using BiiSoft.FileStorages;
using System;
using BiiSoft.Folders;
using BiiSoft.Items;

namespace BiiSoft.Units
{
    public class UnitManager : ItemFieldManagerBase<Unit>, IUnitManager
    {
        public UnitManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<Unit, Guid> repository) : base(appFolders, fileStorageManager, unitOfWorkManager, repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "Unit";

        protected override Unit CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return Unit.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
