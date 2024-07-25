using System.Threading.Tasks;
using Abp.Application.Services;
using BiiSoft.Authorization.Accounts.Dto;
using BiiSoft.Users.Dto;

namespace BiiSoft.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
        Task<ResetPasswordOutput> ResetPassword(ResetPasswordInput input);
        Task SendPasswordResetCode(SendPasswordResetCodeInput input);

        Task<ImpersonateOutput> Impersonate(ImpersonateInput input);
        Task<ImpersonateOutput> BackToImpersonator();
        Task<SwitchToLinkedAccountOutput> SwitchToLinkedAccount(SwitchToLinkedAccountInput input);
        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
