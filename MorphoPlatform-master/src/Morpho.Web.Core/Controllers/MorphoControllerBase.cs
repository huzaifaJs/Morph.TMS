using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Morpho.Controllers
{
    public abstract class MorphoControllerBase: AbpController
    {
        protected MorphoControllerBase()
        {
            LocalizationSourceName = MorphoConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
