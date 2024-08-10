using System;
using System.Net;
using System.Threading.Tasks;
using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;
using Abp.UI;
using BiiSoft.Controllers;
using BiiSoft.BFiles;
using Abp.Authorization;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Domain.Uow;
using System.Linq;
using BiiSoft.Authorization;

namespace BiiSoft.Web.Controllers
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class BFileController : BiiSoftControllerBase
    {
        private readonly IBFileManager _bFileManager;

        public BFileController(
            IBFileManager bFileManager
        )
        {
            _bFileManager = bFileManager;
        }

        [DisableAuditing]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ActionResult> Index(Guid id)
        {
            try
            {
                var bFile = await _bFileManager.DownLoad(AbpSession.TenantId, id);

                return File(bFile.Stream, bFile.ContentType, fileDownloadName: bFile.FileName);
            }
            catch (UserFriendlyException ex)
            {
                throw new UserFriendlyException(L(ex.Message));
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        [AbpMvcAuthorize]
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

            if (file.Length > BiiSoftConsts.MaxFileSize)
            {
                throw new UserFriendlyException(L("FileSizeLimit", BiiSoftConsts.MaxFileSize));
            }

            if (!BiiSoftConsts.FileMineTypes.Values.Any(s => s != file.ContentType))
            {
                throw new Abp.UI.UserFriendlyException(L("IsNotValid", L("FileType")));
            }

            try
            {
                return await _bFileManager.Upload(AbpSession.TenantId, AbpSession.UserId.Value, input.UploadSource, file, input.DisplayName);
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

    }
}