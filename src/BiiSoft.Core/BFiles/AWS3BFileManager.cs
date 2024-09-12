using Abp.Dependency;
using Abp.Domain.Uow;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Abp.UI;
using Microsoft.Extensions.Configuration;
using BiiSoft.BFiles;
using BiiSoft.Configuration;
using BiiSoft.Enums;
using BiiSoft.Extensions;
using BiiSoft;
using System.Collections.Generic;
using Amazon.S3.Util;
using BiiSoft.BFiles.Dto;

namespace CorarlERP.FileUploads
{
    public class AWS3BFileManager : BFileManagerBase, IAWS3BFileManager, ITransientDependency
    {
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IAmazonS3 _s3Client;
        public AWS3BFileManager(
            IAppConfigurationAccessor configurationAccessor)
        {
            _appConfiguration = configurationAccessor.Configuration;
            _s3Client = new AmazonS3Client(AWSAccessKey, AWSSecretKey, Amazon.RegionEndpoint.GetBySystemName(AWSRegion));
        }

        public string AWSAccessKey => _appConfiguration["AWS:S3:AccessKey"];
        public string AWSSecretKey => _appConfiguration["AWS:S3:SecretKey"];
        public string AWSRegion => _appConfiguration["AWS:S3:Region"];
        public string AWSBucketName => _appConfiguration["AWS:S3:BucketNameUpload"];

        public async Task<BFile> Upload(int? tenatId, long curentUserId, UploadSource uploadSource, IFormFile file, string displayName)
        {
            var imageExtension = Path.GetExtension(file.FileName).Replace(".", "").ToLowerInvariant();

            var path = BuildPath(tenatId, file);
            var bucketName = this.AWSBucketName;

            await CheckS3Bucket(bucketName);

            var request = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = path.FilePath,
                InputStream = file.OpenReadStream()
            };
            request.Metadata.Add("Content-Type", file.ContentType);
            await _s3Client.PutObjectAsync(request);
            
            var bFile = BFile.Create(
                tenatId,
                curentUserId,
                path.FileName,
                displayName,
                bucketName,
                path.FilePath,
                file.ContentType,
                imageExtension,
                FileStorage.AWS,
                uploadSource);

            return bFile;

        }

        public async Task<BFile> UploadImage(int? tenatId, long curentUserId, UploadSource uploadSource, IFormFile file, string displayName, int resizeMaxWidth)
        {
            var imageExtension = Path.GetExtension(file.FileName).Replace(".", "").ToLowerInvariant();
            if (!BiiSoftConsts.ImageMineTypes.ContainsKey(imageExtension)) throw new UserFriendlyException("InvalidImage");

            Stream saveStream =  file.OpenReadStream();

            var path = BuildPath(tenatId, file);
            var bucketName = this.AWSBucketName;

            await CheckS3Bucket(bucketName);

            var request = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = path.FilePath,
                InputStream = saveStream
            };
            request.Metadata.Add("Content-Type", file.ContentType);
            await _s3Client.PutObjectAsync(request);

            var bFile = BFile.Create(
                tenatId,
                curentUserId,
                path.FileName,
                displayName,
                bucketName,
                path.FilePath,
                file.ContentType,
                imageExtension,
                FileStorage.AWS,
                uploadSource);

            return bFile;

        }


        public async Task<BFileDownloadOutput> Download(string mainFolderName, string storageFilePath, string contentType)
        {
            await CheckS3Bucket(mainFolderName);

            var s3Object = await _s3Client.GetObjectAsync(mainFolderName, storageFilePath);

            return new BFileDownloadOutput()
            {
                Stream = s3Object.ResponseStream,
                ContentType = contentType
            };
        }

        private async Task CheckS3Bucket(string bucketName)
        {
            var bucketExists = await  AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExists) throw new UserFriendlyException("MainFolderNotExists"); 
        }

        public async Task<bool> Delete(string mainFolderName, string storageFilePath)
        {           
            try
            {
                await CheckS3Bucket(AWSBucketName);

                await _s3Client.DeleteObjectAsync(mainFolderName, storageFilePath);
                return true;
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

    }
}
