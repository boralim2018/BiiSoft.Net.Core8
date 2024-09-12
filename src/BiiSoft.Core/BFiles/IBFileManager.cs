using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BiiSoft.Enums;
using BiiSoft.BFiles.Dto;

namespace BiiSoft.BFiles
{
    public interface IBFileManager
    {
        Task Delete(int? tenantId, Guid fileId);
        Task<BFileDownloadOutput> DownLoad(int? tenantId, Guid fileId);
        Task<BFileUploadOutput> Upload(int? tenantId, long curentUserId, UploadSource uploadSource, IFormFile file, string displayName);
        Task<BFileUploadOutput> UploadImage(int? tenantId, long curentUserId, UploadSource uploadSource, IFormFile file, string displayName, int resizeMaxWidth);
    }
}
