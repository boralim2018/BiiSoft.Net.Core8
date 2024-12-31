using BiiSoft.Items;
using System;

namespace BiiSoft.Cameras
{
    public class CameraManager : ItemFieldManagerBase<Camera>, ICameraManager
    {
        public CameraManager(
            IBiiSoftRepository<Camera, Guid> repository) : base(repository)
        {

        }

        #region override
        protected override string InstanceKeyName => "Camera";

        protected override Camera CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return Camera.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
