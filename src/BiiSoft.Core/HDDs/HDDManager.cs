using System;
using BiiSoft.Items;

namespace BiiSoft.HDDs
{
    public class HDDManager : ItemFieldManagerBase<HDD>, IHDDManager
    {
        public HDDManager(
            IBiiSoftRepository<HDD, Guid> repository) : base(repository)
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
