using System;
using BiiSoft.Items;

namespace BiiSoft.RAMs
{
    public class RAMManager : ItemFieldManagerBase<RAM>, IRAMManager
    {
        public RAMManager(
            IBiiSoftRepository<RAM, Guid> repository) : base(repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "RAM";

        protected override RAM CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return RAM.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
