using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using BiiSoft.BFiles.Dto;
using Microsoft.AspNetCore.Http;

namespace BiiSoft.BFiles
{
    public abstract class BFileManagerBase
    {
        protected FilePathOutput BuildPath(int? tenantId, IFormFile file)
        {
            var tenantPath = tenantId.HasValue ? $"Tenant_{tenantId}" : "Host";

            //202410
            var subFolderName = DateTime.UtcNow.ToString("yyyyMM");

            //BFiles/Tenant_1/202410
            var folderName = Path.Combine(BiiSoftConsts.BFilesFolder, tenantPath, subFolderName);
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var storageName = $"{Guid.NewGuid().ToString("N").ToUpperInvariant()}.{fileName.Split('.').LastOrDefault()}";

            //BFiles/Tenant_1/202410/0000-0000-0000-0000-0000.jpg
            var fullPath = Path.Combine(folderName, storageName);

            return new FilePathOutput
            {
                FilePath = fullPath,
                StarageName = storageName,
                FileName = fileName,
                FolderName = folderName
            };
        }


    }
}
