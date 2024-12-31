using System;
using BiiSoft.Items;

namespace BiiSoft.FieldCs
{
    public class FieldCManager : ItemFieldManagerBase<FieldC>, IFieldCManager
    {
        public FieldCManager(
            IBiiSoftRepository<FieldC, Guid> repository) : base(repository)
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
