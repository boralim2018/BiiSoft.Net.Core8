using System;
using BiiSoft.Items;

namespace BiiSoft.FieldAs
{
    public class FieldAManager : ItemFieldManagerBase<FieldA>, IFieldAManager
    {
        public FieldAManager(
            IBiiSoftRepository<FieldA, Guid> repository) : base(repository)
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
