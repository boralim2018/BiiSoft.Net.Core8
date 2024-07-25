using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BiiSoft.EntityFrameworkCore;
using BiiSoft.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace BiiSoft.Web.Tests
{
    [DependsOn(
        typeof(BiiSoftWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class BiiSoftWebTestModule : AbpModule
    {
        public BiiSoftWebTestModule(BiiSoftEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BiiSoftWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(BiiSoftWebMvcModule).Assembly);
        }
    }
}