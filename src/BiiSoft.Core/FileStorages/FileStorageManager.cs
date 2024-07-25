using Abp.Dependency;
using Amazon.Runtime;
using Amazon.S3;
using BiiSoft.Configuration;
using BiiSoft.Folders;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiiSoft.Enums;
using Microsoft.AspNetCore.Http;

namespace BiiSoft.FileStorages
{
    public class FileStorageManager: IFileStorageManager, ITransientDependency
    {
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IBaseFileStorageManager _fileStorageManager;
        protected readonly IAppFolders _appFolders;
        public FileStorageManager()
        {
            _appConfiguration = IocManager.Instance.Resolve<IAppConfigurationAccessor>().Configuration;

            _fileStorageManager = _appConfiguration["AWS:S3:Enable"].ToLower() == "true" ?
                                  new AWSFileStorageManager() :
                                  new LocalFileStorageManager();
        }

        public async Task<Stream> DownloadTempFile(string fileToken)
        {
            return await _fileStorageManager.DownloadTempFile(fileToken);
        }

        public async Task<string> ReadTemplate(string templateName)
        {
            return await _fileStorageManager.ReadTemplate(templateName);
        }

        public async Task UploadTempFile(string fileToken, IFormFile file)
        {
            await _fileStorageManager.UploadTempFile(fileToken, file);
        }

        public async Task UploadTempFile(string fileToken, byte[] bytes)
        {
            await _fileStorageManager.UploadTempFile(fileToken, bytes);
        }

        public async Task UploadTempFile(string fileToken, ExcelPackage excelPackage)
        {
            await _fileStorageManager.UploadTempFile(fileToken, excelPackage);
        }

        public async Task<ExcelPackage> DownloadExcel(string fileToken)
        {
            var fileSteam = await _fileStorageManager.DownloadTempFile(fileToken);
            var package = new ExcelPackage(fileSteam);
            return package;
        }

    }
}
