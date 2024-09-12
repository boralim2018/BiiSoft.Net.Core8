using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Uow;
using System.Transactions;
using System.Threading.Tasks;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Abp.Extensions;
using Abp.Dependency;
using Microsoft.Extensions.Configuration;
using Abp.Domain.Repositories;
using BiiSoft.Configuration;
using BiiSoft.Enums;
using Microsoft.EntityFrameworkCore;
using BiiSoft.BFiles.Dto;

namespace BiiSoft.BFiles
{
    public class BFileManager : IBFileManager, ITransientDependency
    {
        private readonly IRepository<BFile, Guid> _bFileRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ILocalBFileManager _localBFileManager;
        private readonly IAWS3BFileManager _aws3BFileManager;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly bool AwsS3Enable;

        public BFileManager()
        {
            _appConfiguration = IocManager.Instance.Resolve<IAppConfigurationAccessor>().Configuration;
            _unitOfWorkManager = IocManager.Instance.Resolve<IUnitOfWorkManager>();            
            _bFileRepository = IocManager.Instance.Resolve<IRepository<BFile, Guid>>();
            _localBFileManager = IocManager.Instance.Resolve<ILocalBFileManager>();
            _aws3BFileManager = IocManager.Instance.Resolve<IAWS3BFileManager>();

            AwsS3Enable = _appConfiguration["AWS:S3:Enable"].ToLower() == "true";
        }

        [UnitOfWork(IsDisabled = true)]
        public async Task<BFileUploadOutput> Upload(int? tenantId, long curentUserId, UploadSource uploadSource, IFormFile file, string diplayName)
        {

            BFile bFile = null;

            if (AwsS3Enable)
            {
                bFile = await _aws3BFileManager.Upload(tenantId, curentUserId, uploadSource, file, diplayName);
            }
            else 
            {
                bFile = await _localBFileManager.Upload(tenantId, curentUserId, uploadSource, file, diplayName);
            }

            try
            {
                await CreateFileAsync(tenantId, bFile);
            }
            catch (Exception ex)
            {
                if (AwsS3Enable)
                {
                    await _aws3BFileManager.Delete(bFile.StorageFolder, bFile.FilePath);
                }
                else 
                {
                    await _localBFileManager.Delete(bFile.StorageFolder, bFile.FilePath);
                }

                throw new UserFriendlyException(ex.Message);
            }

            return new BFileUploadOutput()
            {
                FileName = bFile.Name,
                FileType = bFile.FileType,
                Id = bFile.Id
            };
        }

        private async Task<BFile> CreateFileAsync(int? tenantId, BFile file)
        {
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(tenantId))
                {
                    await _bFileRepository.InsertAsync(file);
                }

               await uow.CompleteAsync();
            }

            return file;
        }


        [UnitOfWork(IsDisabled = true)]
        public async Task<BFileUploadOutput> UploadImage(int? tenantId, long curentUserId, UploadSource uploadSource, IFormFile file, string diplayName, int resizeMaxWidth)
        {

            BFile bFile = null;

            if (AwsS3Enable)
            {
                bFile = await _aws3BFileManager.UploadImage(tenantId, curentUserId, uploadSource, file, diplayName, resizeMaxWidth);
            }
            else
            {
                bFile = await _localBFileManager.UploadImage(tenantId, curentUserId, uploadSource, file, diplayName, resizeMaxWidth);
            }

            try
            {
                await CreateFileAsync(tenantId, bFile);
            }
            catch (Exception ex)
            {
                if (AwsS3Enable)
                {
                    await _aws3BFileManager.Delete(bFile.StorageFolder, bFile.FilePath);
                }
                else
                {
                    await _localBFileManager.Delete(bFile.StorageFolder, bFile.FilePath);
                }

                throw new UserFriendlyException(ex.Message);
            }

            return new BFileUploadOutput()
            {
                FileName = bFile.Name,
                FileType = bFile.FileType,
                Id = bFile.Id
            };
        }

        private async Task<BFile> GetFileAsync(int? tenantId, Guid fileId)
        {
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(tenantId))
                {
                    return await _bFileRepository.GetAll().AsNoTracking().FirstOrDefaultAsync(s => s.Id == fileId);
                }
            }
        }

        [UnitOfWork(IsDisabled = true)]
        public async Task Delete(int? tenantId, Guid fileId)
        {
            var bFile = await GetFileAsync(tenantId, fileId);
            if (bFile == null) return;

            try
            {
                if (bFile.FileStorage == FileStorage.AWS)
                {
                    await _aws3BFileManager.Delete(bFile.StorageFolder, bFile.FilePath);
                }
                else 
                {
                    await _localBFileManager.Delete(bFile.StorageFolder, bFile.FilePath);
                }

                using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
                {
                    using (_unitOfWorkManager.Current.SetTenantId(tenantId))
                    {
                        await _bFileRepository.DeleteAsync(bFile);
                    }
                    await uow.CompleteAsync();
                }
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        [UnitOfWork(IsDisabled = true)]
        public async Task<BFileDownloadOutput> DownLoad(int? tenantId, Guid fileId)
        {
            var bFile = await GetFileAsync(tenantId, fileId);

            if (bFile == null) return null;

            BFileDownloadOutput result = null;

            if (bFile.FileStorage == FileStorage.AWS)
            {
                result = await _aws3BFileManager.Download(bFile.StorageFolder, bFile.FilePath, bFile.FileType);
            }
            else if (bFile.FileStorage == FileStorage.Local)
            {
                result = await _localBFileManager.Download(bFile.StorageFolder, bFile.FilePath, bFile.FileType);
            }
           
            if(result != null)  result.FileName = bFile.DisplayName;

            return result;
        }


    }
}
