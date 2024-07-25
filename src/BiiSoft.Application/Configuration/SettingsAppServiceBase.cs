using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Net.Mail;
using BiiSoft.Configuration.Dto;

namespace BiiSoft.Configuration
{
    public abstract class SettingsAppServiceBase : BiiSoftAppServiceBase
    {
        protected readonly IEmailSender _emailSender;

        protected SettingsAppServiceBase()
        {
            _emailSender = IocManager.Instance.Resolve<IEmailSender>();
        }

        #region Send Test Email

        public async Task SendTestEmail(SendTestEmailInput input)
        {
            await _emailSender.SendAsync(
                input.EmailAddress,
                L("TestEmail_Subject", input.CompanyName),
                L("TestEmail_Body")
            );
        }

        #endregion
    }
}
