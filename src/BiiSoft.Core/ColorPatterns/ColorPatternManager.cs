using Abp.Domain.Uow;
using BiiSoft.FileStorages;
using System;
using BiiSoft.Folders;
using BiiSoft.Items;

namespace BiiSoft.ColorPatterns
{
    public class ColorPatternManager : ItemFieldManagerBase<ColorPattern>, IColorPatternManager
    {
        public ColorPatternManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<ColorPattern, Guid> repository) : base(appFolders, fileStorageManager, unitOfWorkManager, repository) 
        {

        }

        #region override
        protected override string InstanceKeyName => "ColorPattern";
       
        protected override ColorPattern CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return ColorPattern.Create(tenantId, userId, name, displayName, code);
        }
    }
}
