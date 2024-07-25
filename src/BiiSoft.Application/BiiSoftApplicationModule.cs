using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BiiSoft.Authorization;

namespace BiiSoft
{
    [DependsOn(
        typeof(BiiSoftCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class BiiSoftApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<BiiSoftAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(BiiSoftApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}
