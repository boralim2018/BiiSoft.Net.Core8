using System;
using BiiSoft.Items;

namespace BiiSoft.VGAs
{
    public class VGAManager : ItemFieldManagerBase<VGA>, IVGAManager
    {
        public VGAManager(
            IBiiSoftRepository<VGA, Guid> repository) : base(repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "VGA";

        protected override VGA CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return VGA.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
