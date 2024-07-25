using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using BiiSoft.Users.Dto;
using BiiSoft.Users.Profiles.Dto;

namespace BiiSoft.Users.Profiles
{
    public interface IProfileAppService : IApplicationService
    {
        Task<CurrentUserProfileEditDto> GetCurrentUserProfileForEdit();

        Task UpdateCurrentUserProfile(CurrentUserProfileEditDto input);
        
        Task ChangePassword(ChangePasswordInput input);

        Task<GetPasswordComplexitySettingOutput> GetPasswordComplexitySetting();

        //Task<ProfilePictureOutput> GetFriendProfilePictureById(GetFriendProfilePictureByIdInput input);

        Task<UpdateGoogleAuthenticatorKeyOutput> UpdateGoogleAuthenticatorKey();

        Task SendVerificationSms();

        Task VerifySmsCode(VerifySmsCodeInputDto input);

    }
}
