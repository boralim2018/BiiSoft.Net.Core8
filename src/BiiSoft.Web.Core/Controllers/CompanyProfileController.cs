using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Domain.Uow;
using Abp.Timing;
using Abp.UI;
using BiiSoft.Authorization;
using BiiSoft.BFiles;
using BiiSoft.Controllers;
using BiiSoft.MultiTenancy;
using BiiSoft.MultiTenancy.Dto;

namespace BiiSoft.Web.Controllers
{
    [AbpAuthorize(PermissionNames.Pages_Company_CompanySetting_Edit)]
    public class CompanyProfileController : BiiSoftControllerBase
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly TenantManager _tenantManager;
        private readonly IBFileManager _bFileManager;
        public CompanyProfileController(
                IBFileManager bFileManager,
                TenantManager tenantManager,
                IUnitOfWorkManager unitOfWorkManager
            )
        {
            _bFileManager = bFileManager;
            _tenantManager = tenantManager;
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
                await UpdateProfilePicture(new UpdateLogoInput { LogoId = result.Id });
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
        private async Task UpdateProfilePicture(UpdateLogoInput input)
        {
            var tenantId = AbpSession.TenantId;
            var userId = AbpSession.UserId.Value;

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(tenantId))
                {
                    Guid? oldLogoId = null;
                    bool success = false;
                    try
                    {
                        var tenant = await _tenantManager.GetByIdAsync(tenantId.Value);
                        oldLogoId = tenant.LogoId;

                        tenant.SetLogo(input.LogoId);
                        tenant.LastModifierUserId = userId;
                        tenant.LastModificationTime = Clock.Now;
                        await _tenantManager.UpdateAsync(tenant);
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        throw new UserFriendlyException(ex.Message);
                    }
                    finally
                    {
                        if (success && oldLogoId.HasValue)
                        {
                            await _bFileManager.Delete(tenantId, oldLogoId.Value);
                        }
                    }
                }
                await uow.CompleteAsync();
            }
        }
    }
}