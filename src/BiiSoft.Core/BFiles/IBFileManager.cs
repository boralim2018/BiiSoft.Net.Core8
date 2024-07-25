using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BiiSoft.Enums;

namespace BiiSoft.BFiles
{
    public interface IBFileManager
    {
        Task Delete(int? tenantId, Guid galleryId);
        Task<BFileDownloadOutput> DownLoad(int? tenantId, Guid id);
        Task<BFileUploadOutput> Upload(int? tenantId, long curentUserId, UploadSource uploadSource, IFormFile file, string displayName);
        Task<BFileUploadOutput> UploadImage(int? tenantId, long curentUserId, UploadSource uploadSource, IFormFile file, string displayName, int resizeMaxWidth);
    }
}
