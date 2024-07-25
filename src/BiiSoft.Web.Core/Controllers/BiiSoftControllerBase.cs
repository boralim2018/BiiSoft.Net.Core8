using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace BiiSoft.Controllers
{
    public abstract class BiiSoftControllerBase: AbpController
    {
        protected BiiSoftControllerBase()
        {
            LocalizationSourceName = BiiSoftConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
