using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.IO.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Models;
using BiiSoft.Authorization;
using BiiSoft.Authorization.Users;
using BiiSoft.BFiles;
using BiiSoft.BFiles.Dto;
using BiiSoft.Controllers;
using BiiSoft.Users.Profiles.Dto;

namespace BiiSoft.Web.Controllers
{
    [AbpAuthorize(PermissionNames.Pages_Profile)]
    public class UserProfileController : BiiSoftControllerBase
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UserManager _userManager;
        private readonly IBFileManager _bFileManager;
        public UserProfileController(
                IBFileManager bFileManager,
                UserManager userManager,
                IUnitOfWorkManager unitOfWorkManager
            )
        {
            _bFileManager = bFileManager;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
        }

        [AbpAuthorize]
        [DisableAuditing]
        [UnitOfWork(IsDisabled = true)]
        public async Task<BFileUploadOutput> Upload(FileUploadInput input)
        {
            var file = Request.Form.Files.First();

            //Check input                
            if (file == null)
            {
                throw new Abp.UI.UserFriendlyException(L("IsNotValid", L("File")));
            }

            if (file.Length > BiiSoftConsts.MaxProfilePictureSize)
            {
                throw new UserFriendlyException(L("FileSizeLimit", BiiSoftConsts.MaxProfilePictureSize));
            }

            if (!BiiSoftConsts.ImageMineTypes.Values.Any(s => s != file.ContentType))
            {
                throw new Abp.UI.UserFriendlyException(L("IsNotValid", L("FileType")));
            }

            try
            {
                var result = await _bFileManager.UploadImage(AbpSession.TenantId, AbpSession.UserId.Value, input.UploadSource, file, input.DisplayName, BiiSoftConsts.MaxProfilePictureWidth);
                await UpdateProfilePicture(new UpdateProfilePictureInput { ProfilePictureId = result.Id });
                return result;
            }
            catch (UserFriendlyException ex)
            {
                throw new Abp.UI.UserFriendlyException(L(ex.Message));
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(L("CannotUpload"));
            }

        }

        [UnitOfWork(IsDisabled = true)]
        private async Task UpdateProfilePicture(UpdateProfilePictureInput input)
        {
            var tenantId = AbpSession.TenantId;
            var userId = AbpSession.UserId.Value;

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(tenantId))
                {
                    Guid? oldProfilePictureId = null;
                    bool success = false;
                    try
                    {
                        var user = await _userManager.GetUserByIdAsync(userId);
                        oldProfilePictureId = user.ProfilePictureId;

                        user.SetProfilePicture(input.ProfilePictureId);
                        await _userManager.UpdateAsync(user);
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        throw new UserFriendlyException(ex.Message);
                    }
                    finally
                    {
                        if (success && oldProfilePictureId.HasValue)
                        {
                            await _bFileManager.Delete(tenantId, oldProfilePictureId.Value);
                        }
                    }
                }
                await uow.CompleteAsync();
            }
        }
    }
}