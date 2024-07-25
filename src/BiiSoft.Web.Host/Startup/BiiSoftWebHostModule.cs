using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BiiSoft.Configuration;

namespace BiiSoft.Web.Host.Startup
{
    [DependsOn(
       typeof(BiiSoftWebCoreModule))]
    public class BiiSoftWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public BiiSoftWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BiiSoftWebHostModule).GetAssembly());
        }
    }
}
