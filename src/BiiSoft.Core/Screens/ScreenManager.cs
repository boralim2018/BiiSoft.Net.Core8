using System;
using BiiSoft.Items;

namespace BiiSoft.Screens
{
    public class ScreenManager : ItemFieldManagerBase<Screen>, IScreenManager
    {
        public ScreenManager(
            IBiiSoftRepository<Screen, Guid> repository) : base(repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "Screen";

        protected override Screen CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return Screen.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
