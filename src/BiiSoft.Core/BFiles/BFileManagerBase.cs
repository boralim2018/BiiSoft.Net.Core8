using System;
using System.Text;
using System.Net.Http.Headers;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;
using Amazon.Runtime.Internal.Util;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Net;
using BiiSoft.BFiles.Dto;

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
