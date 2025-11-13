using Abp.AspNetCore.Mvc.ViewComponents;

namespace Morpho.Web.Views
{
    public abstract class MorphoViewComponent : AbpViewComponent
    {
        protected MorphoViewComponent()
        {
            LocalizationSourceName = MorphoConsts.LocalizationSourceName;
        }
    }
}
