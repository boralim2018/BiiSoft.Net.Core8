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

namespace BiiSoft.BFiles
{
    public abstract class BFileManagerBase
    {
        protected FilePathOutput BuildPath(int? tenantId, IFormFile file)
        {
            var tenantPath = tenantId.HasValue ? $"Tenant_{tenantId}" : "Host";

            var dateTime = DateTime.UtcNow;
            var subFolderName = dateTime.ToString("yyyyMM");

            //BFiles/Tenant_1/2022_06
            var folderName = Path.Combine(BiiSoftConsts.BFilesFolder, tenantPath, subFolderName);
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var storageName = $"{Guid.NewGuid().ToString("N").ToUpperInvariant()}.{fileName.Split('.').LastOrDefault()}";

            //BFiles/Tenant_1/2022_06/0000-0000-0000-0000-0000.jpg
            var fullPath = Path.Combine(folderName, storageName);

            return new FilePathOutput
            {
                FilePath = fullPath,
                StarageName = storageName,
                FileName = fileName,
                FolderName = folderName
            };
        }

        //protected Dictionary<string, SKEncodedImageFormat> SKEncodedImageFormatDic =>
        //new Dictionary<string, SKEncodedImageFormat>
        //{
        //    {"bmp", SKEncodedImageFormat.Bmp },
        //    //{"cod", SKEncodedImageFormat.Cod },
        //    {"gif", SKEncodedImageFormat.Gif },
        //    //{"ief", SKEncodedImageFormat.Ief },
        //    {"jpe", SKEncodedImageFormat.Jpeg },
        //    {"jpeg", SKEncodedImageFormat.Jpeg },
        //    {"jpg", SKEncodedImageFormat.Jpeg },
        //    //{"jfif",SKEncodedImageFormat.Jfif},
        //    //{"svg", SKEncodedImageFormat.Svg },
        //    //{"tif",SKEncodedImageFormat.Tif },
        //    //{"tiff", SKEncodedImageFormat.Tiff },
        //    //{"ras", SKEncodedImageFormat.Tas},
        //    //{"cmx",SKEncodedImageFormat.Cmx },
        //    {"ico", SKEncodedImageFormat.Ico },
        //    {"png", SKEncodedImageFormat.Png },
        //    //{"pnm", SKEncodedImageFormat.Pnm},
        //    //{"pbm", SKEncodedImageFormat.Pbm },
        //    //{"pgm", SKEncodedImageFormat.Mgm},
        //    //{"ppm", SKEncodedImageFormat.Ppm },
        //    //{"rgb", SKEncodedImageFormat.Rgb},
        //    //{"xbm", SKEncodedImageFormat.Xbm},
        //    //{"xpm", SKEncodedImageFormat.Xpm },
        //    //{"xwd", SKEncodedImageFormat.Xwd}
        //    {"ktx", SKEncodedImageFormat.Ktx},
        //    {"wbmp", SKEncodedImageFormat.Wbmp },
        //    {"webp", SKEncodedImageFormat.Webp },
        //    {"pkm", SKEncodedImageFormat.Pkm },
        //    {"astc", SKEncodedImageFormat.Astc },
        //    {"dng", SKEncodedImageFormat.Dng },
        //    {"heif", SKEncodedImageFormat.Heif },
        //    {"avif", SKEncodedImageFormat.Avif }
        //};


    }
}
