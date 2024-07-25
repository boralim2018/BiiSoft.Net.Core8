using Abp.Dependency;
using Abp.Domain.Services;
using BiiSoft.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.BFiles
{
    public interface IAWS3BFileManager
    {
        Task<BFile> Upload(int? tenatId, long curentUserId, UploadSource uploadSource, IFormFile file, string displayName);
        Task<BFile> UploadImage(int? tenatId, long curentUserId, UploadSource uploadSource, IFormFile file, string displayName, int resizeMaxWidth);
        Task<BFileDownloadOutput> Download(string mainFolderName, string storageFilePath, string contentType);
        Task<bool> Delete(string mainFolderName, string storageFilePath);
    }
}
