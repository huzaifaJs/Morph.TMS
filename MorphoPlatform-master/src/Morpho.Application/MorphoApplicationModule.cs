using Abp.AutoMapper;
using Abp.FluentValidation;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Morpho.Authorization;

namespace Morpho
{
    [DependsOn(
        typeof(MorphoCoreModule),
        typeof(AbpFluentValidationModule),
        typeof(AbpAutoMapperModule))]
    public class MorphoApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<MorphoAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(MorphoApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
