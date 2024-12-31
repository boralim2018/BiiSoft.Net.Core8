using BiiSoft.Items;
using System;

namespace BiiSoft.CPUs
{
    public class CPUManager : ItemFieldManagerBase<CPU>, ICPUManager
    {
        public CPUManager(
            IBiiSoftRepository<CPU, Guid> repository) : base(repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "CPU";

        protected override CPU CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return CPU.Create(tenantId, userId, name, displayName, code);
        }
        #endregion
    }
}