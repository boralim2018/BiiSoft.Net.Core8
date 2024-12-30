using Abp.Domain.Uow;
using BiiSoft.FileStorages;
using System;
using BiiSoft.Folders;
using BiiSoft.Items;

namespace BiiSoft.VGAs
{
    public class VGAManager : ItemFieldManagerBase<VGA>, IVGAManager
    {
        public VGAManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<VGA, Guid> repository) : base(appFolders, fileStorageManager, unitOfWorkManager, repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "VGA";

        protected override VGA CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return VGA.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
