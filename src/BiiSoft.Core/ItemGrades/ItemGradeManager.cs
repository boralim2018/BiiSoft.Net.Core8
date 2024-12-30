using Abp.Domain.Uow;
using BiiSoft.FileStorages;
using System;
using BiiSoft.Folders;
using BiiSoft.Items;

namespace BiiSoft.ItemGrades
{
    public class ItemGradeManager : ItemFieldManagerBase<ItemGrade>, IItemGradeManager
    {
        public ItemGradeManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBiiSoftRepository<ItemGrade, Guid> repository) : base(appFolders, fileStorageManager, unitOfWorkManager, repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "ItemGrade";

        protected override ItemGrade CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return ItemGrade.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
