using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OfficeOpenXml;
using Microsoft.AspNetCore.Http;

namespace BiiSoft.FileStorages
{
    public interface IBaseFileStorageManager
    {
        Task<string> ReadTemplate(string templateName);
        Task<Stream> DownloadTempFile(string fileToken);
        Task UploadTempFile(string fileToken, IFormFile file);
        Task UploadTempFile(string fileToken, byte[] bytes);
        Task UploadTempFile(string fileToken, ExcelPackage excelPackage);
    }

    public interface IFileStorageManager : IBaseFileStorageManager
    {
        Task<ExcelPackage> DownloadExcel(string fileToken);
    }
}
