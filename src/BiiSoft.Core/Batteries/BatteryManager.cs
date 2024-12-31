using BiiSoft.Items;
using System;

namespace BiiSoft.Batteries
{
    public class BatteryManager : ItemFieldManagerBase<Battery>, IBatteryManager
    {
        public BatteryManager(
            IBiiSoftRepository<Battery, Guid> repository) : base(repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "Battery";

        protected override Battery CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return Battery.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
