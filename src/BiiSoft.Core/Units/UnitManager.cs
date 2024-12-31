using System;
using BiiSoft.Items;

namespace BiiSoft.Units
{
    public class UnitManager : ItemFieldManagerBase<Unit>, IUnitManager
    {
        public UnitManager(
            IBiiSoftRepository<Unit, Guid> repository) : base(repository)
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
