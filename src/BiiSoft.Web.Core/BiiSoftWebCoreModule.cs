using System;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.SignalR;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using BiiSoft.Authentication.JwtBearer;
using BiiSoft.Configuration;
using BiiSoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using BiiSoft.EntityFrameworkCore.Repositories;
using Abp.Dependency;
using BiiSoft.Web.Configuration;
using Abp.Configuration.Startup;
using Abp.IO;
using System.IO;
using BiiSoft.Folders;
using BiiSoft.Debugging;
//using Abp.Hangfire.Configuration;

namespace BiiSoft
{
    [DependsOn(
         typeof(BiiSoftApplicationModule),
         typeof(BiiSoftEntityFrameworkModule),
         typeof(AbpAspNetCoreModule),
         typeof(AbpAspNetCoreSignalRModule)
     )]
    public class BiiSoftWebCoreModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public BiiSoftWebCoreModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                BiiSoftConsts.ConnectionStringName
            );

            // Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(BiiSoftApplicationModule).GetAssembly()
                 );

            ConfigureTokenAuth();

            Configuration.ReplaceService<IAppConfigurationAccessor, AppConfigurationAccessor>();
            //Configuration.BackgroundJobs.UseHangfire();

            IocManager.Register(typeof(IBiiSoftRepository<>), typeof(BiiSoftRepository<>), DependencyLifeStyle.Transient);
            IocManager.Register(typeof(IBiiSoftRepository<,>), typeof(BiiSoftRepository<,>), DependencyLifeStyle.Transient);
        }

        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();
            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = DebugHelper.IsDebug ? TimeSpan.FromDays(365) : TimeSpan.FromDays(1);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BiiSoftWebCoreModule).GetAssembly());

        }

        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(BiiSoftWebCoreModule).Assembly);

            InitAppFolders();
        }

        private void InitAppFolders()
        {
            var appFolders = IocManager.Resolve<AppFolders>();

            appFolders.AssetsFolder = Path.Combine(_env.WebRootPath, "Assets");
            appFolders.ImagesFolder = Path.Combine(_env.WebRootPath, "Assets", "Images");
            appFolders.FontsFolder = Path.Combine(_env.WebRootPath, "Assets", "Fonts");
            appFolders.UserProfilesFolder = Path.Combine(_env.WebRootPath, "Assets", "Images", "UserProfiles");
            appFolders.AppLogsFolder = Path.Combine(_env.ContentRootPath, "App_Data", "Logs");

            appFolders.TemplateFolder = Path.Combine(_env.WebRootPath, "Templates");
            appFolders.DownloadFolder = Path.Combine(_env.WebRootPath, "Downloads");
            appFolders.DownloadUrl = @"/file/DownloadTempFile";

            try
            {
                DirectoryHelper.CreateIfNotExists(appFolders.DownloadFolder);
            }
            catch 
            { 
            
            }
        }
    }
}
