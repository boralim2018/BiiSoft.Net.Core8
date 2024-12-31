using System;
using BiiSoft.Items;

namespace BiiSoft.FieldBs
{
    public class FieldBManager : ItemFieldManagerBase<FieldB>, IFieldBManager
    {
        public FieldBManager(
            IBiiSoftRepository<FieldB, Guid> repository) : base(repository)
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
