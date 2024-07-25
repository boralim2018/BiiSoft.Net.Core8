using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Abp.UI;
using OfficeOpenXml;
using BiiSoft.Folders;
using Microsoft.AspNetCore.Http;
using Abp.IO.Extensions;
using Abp.Dependency;

namespace BiiSoft.FileStorages
{
    public class LocalFileStorageManager : IBaseFileStorageManager
    {
        protected readonly IAppFolders _appFolders;

        public LocalFileStorageManager()
        {
            _appFolders = IocManager.Instance.Resolve<IAppFolders>();
        }

        public async Task<string> ReadTemplate(string templateName)
        {   
            var tokenPath = Path.Combine(_appFolders.TemplateFolder, templateName);
            if (!File.Exists(tokenPath))
            {
                throw new UserFriendlyException("FileNotFound");
            }

            using (StreamReader r = new StreamReader(tokenPath))
            {
                return await r.ReadToEndAsync();
            }  
        }

        public async Task<Stream> DownloadTempFile(string fileToken)
        {
            var filePath = Path.Combine(_appFolders.DownloadFolder, fileToken);
            if (!File.Exists(filePath))
            {
                throw new UserFriendlyException("RequestedFileDoesNotExists");
            }
            var memory = new MemoryStream();

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            File.Delete(filePath);

            return memory;
        }

        public async Task UploadTempFile(string fileToken, IFormFile file)
        {
            var filePath = Path.Combine(_appFolders.DownloadFolder, fileToken);
            await File.WriteAllBytesAsync(filePath, file.OpenReadStream().GetAllBytes());
        }

        public async Task UploadTempFile(string fileToken, byte[] bytes)
        {
            var filePath = Path.Combine(_appFolders.DownloadFolder, fileToken);
            await File.WriteAllBytesAsync(filePath, bytes);
        }

        public async Task UploadTempFile(string fileToken, ExcelPackage excelPackage)
        {
            await Task.Run(() =>
            {
                var filePath = Path.Combine(_appFolders.DownloadFolder, fileToken);
                excelPackage.SaveAs(new FileInfo(filePath));
            });   
        }
    }
}
