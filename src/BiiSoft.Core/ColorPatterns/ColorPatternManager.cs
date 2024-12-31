using BiiSoft.Items;
using System;

namespace BiiSoft.ColorPatterns
{
    public class ColorPatternManager : ItemFieldManagerBase<ColorPattern>, IColorPatternManager
    {
        public ColorPatternManager(
            IBiiSoftRepository<ColorPattern, Guid> repository) : base(repository) 
        {

        }

        #region override
        protected override string InstanceKeyName => "ColorPattern";
       
        protected override ColorPattern CreateInstance(int tenantId, long userId, string name, string displayName, string code)
        {
            return ColorPattern.Create(tenantId, userId, name, displayName, code);
        }

        #endregion
    }
}
