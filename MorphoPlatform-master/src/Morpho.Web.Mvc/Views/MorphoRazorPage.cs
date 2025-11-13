using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Morpho.Web.Views
{
    public abstract class MorphoRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected MorphoRazorPage()
        {
            LocalizationSourceName = MorphoConsts.LocalizationSourceName;
        }
    }
}
