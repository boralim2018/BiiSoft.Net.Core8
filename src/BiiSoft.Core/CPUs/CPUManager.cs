using Abp.Domain.Uow;
using BiiSoft.FileStorages;
using System;
using BiiSoft.Folders;
using BiiSoft.Items;

namespace BiiSoft.CPUs
{
    public class CPUManager : ItemFieldManagerBase<CPU>, ICPUManager
    {
        public CPUManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<CPU, Guid> repository) : base(appFolders, fileStorageManager, unitOfWorkManager, repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "CPU";

        protected override CPU CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return CPU.Create(tenantId, userId, name, displayName, code);
        }
        #endregion
    }
}