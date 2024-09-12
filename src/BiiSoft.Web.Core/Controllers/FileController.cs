using System;
using System.Net;
using System.Threading.Tasks;
using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;
using Abp.UI;
using BiiSoft.FileStorages;
using BiiSoft.Controllers;
using BiiSoft.Folders;
using Abp.Authorization;
using Abp.Web.Models;
using System.IO;
using AngleSharp.Common;
using BiiSoft.BFiles.Dto;

namespace BiiSoft.Web.Controllers
{
    [AbpAuthorize]
    public class FileController : BiiSoftControllerBase
    {
   
        private readonly IFileStorageManager _fileStorageManager;

        public FileController(
            IFileStorageManager fileStorageManager
        )
        {
            _fileStorageManager = fileStorageManager;
        }

        
        private string ConvertExtentionToContentType(string extension)
        {
            return BiiSoftConsts.FileMineTypes.ContainsKey(extension) ? BiiSoftConsts.FileMineTypes[extension] :
                   BiiSoftConsts.ImageMineTypes.ContainsKey(extension) ? BiiSoftConsts.ImageMineTypes[extension] : "";
        }

        [DisableAuditing]
        public async Task<ActionResult> DownloadTempFile(ExportFileOutput file)
        {
            try
            {
                var extensionArray = file.FileName.Contains(".") ? file.FileName.Split(".") : null;

                if (extensionArray == null)
                {
                    throw new UserFriendlyException(L("InvalidExtension"));
                }
                var extension = extensionArray[extensionArray.Length - 1].ToLower();

                var contentType = ConvertExtentionToContentType(extension);

                var fileBytes = await _fileStorageManager.DownloadTempFile(file.FileToken);
                return File(fileBytes, contentType, file.FileName);
            }
            catch (UserFriendlyException ex)
            {
                throw new UserFriendlyException(L(ex.Message));               
            }          
        }


        [DisableAuditing]
        public async Task<JsonResult> ImportExcelFile()
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count <= 0 || Request.Form.Files[0] == null)
                {
                    throw new UserFriendlyException(L("ImportFileError"));
                }
                var file = Request.Form.Files[0];
                var fileInfo = new FileInfo(file.FileName);

                if (file.Length > (5242880 * 10)) //1MB * 10.
                {
                    throw new UserFriendlyException(L("ImportFileExcelWarnSizeLimit"));
                }

                //Check file type & format
                if (fileInfo.Extension.ToLower().Trim() != ".xlsx" && fileInfo.Extension.ToLower().Trim() != ".xls")
                {
                    throw new UserFriendlyException(L("ImportFileWarnExcel"));
                }

                var token = $"{Guid.NewGuid()}.{fileInfo.Extension}";
                await _fileStorageManager.UploadTempFile(token, file);

                return Json(new AjaxResponse(new { fileName = file.FileName, fileToken = token, fileExtension = fileInfo.Extension }));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

    }
}