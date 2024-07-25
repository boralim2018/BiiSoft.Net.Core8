using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Localization;
using Abp.MailKit;
using Abp.Modules;
using Abp.Net.Mail;
using Abp.Net.Mail.Smtp;
using Abp.Reflection.Extensions;
using Abp.Runtime.Security;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using BiiSoft.Authorization.Roles;
using BiiSoft.Authorization.Users;
using BiiSoft.Configuration;
using BiiSoft.Debugging;
using BiiSoft.Emailing;
using BiiSoft.Features;
using BiiSoft.Localization;
using BiiSoft.MultiTenancy;
using BiiSoft.Timing;
using Castle.MicroKernel.Registration;

namespace BiiSoft
{
    [DependsOn(typeof(AbpZeroCoreModule), typeof(AbpMailKitModule))]
    public class BiiSoftCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            BiiSoftLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = BiiSoftConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            // Register feature provider
            Configuration.Features.Providers.Add<AppFeatureProvider>();

            Configuration.Settings.Providers.Add<AppSettingProvider>();
            
            //Configuration.Localization.Languages.Add(new LanguageInfo("fa", "فارسی", "famfamfam-flags ir"));
            
            Configuration.Settings.SettingEncryptionConfiguration.DefaultPassPhrase = BiiSoftConsts.DefaultPassPhrase;
            SimpleStringCipher.DefaultPassPhrase = BiiSoftConsts.DefaultPassPhrase;

            if (DebugHelper.IsDebug)
            {
                //Disabling email sending in debug mode
                Configuration.ReplaceService<IEmailSender, MailKitEmailSender>(DependencyLifeStyle.Transient);
            }

            Configuration.ReplaceService(typeof(IEmailSenderConfiguration), () =>
            {
                Configuration.IocManager.IocContainer.Register(
                    Component.For<IEmailSenderConfiguration, ISmtpEmailSenderConfiguration>()
                             .ImplementedBy<BiiSoftSmtpEmailSenderConfiguration>()
                             .LifestyleTransient()
                );
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BiiSoftCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
