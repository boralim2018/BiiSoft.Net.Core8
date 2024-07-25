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

namespace BiiSoft.BFiles
{
    public class BFileManager : IBFileManager, ITransientDependency
    {
        private readonly IRepository<BFile, Guid> _bFileRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ILocalBFileManager _localBFileManager;
        private readonly IAWS3BFileManager _aws3BFileManager;
        private readonly IConfigurationRoot _appConfiguration;

        public BFileManager()
        {
            _appConfiguration = IocManager.Instance.Resolve<IAppConfigurationAccessor>().Configuration;
            _unitOfWorkManager = IocManager.Instance.Resolve<IUnitOfWorkManager>();            
            _bFileRepository = IocManager.Instance.Resolve<IRepository<BFile, Guid>>();
            _localBFileManager = IocManager.Instance.Resolve<ILocalBFileManager>();
            _aws3BFileManager = IocManager.Instance.Resolve<IAWS3BFileManager>();
        }

        public bool AwsS3Enable => _appConfiguration["AWS:S3:Enable"].ToLower() == "true";

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
                await SaveFileHelper(tenantId, bFile);
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
                await SaveFileHelper(tenantId, bFile);
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


        private string GetFileSizeInBytes(long TotalBytes)
        {
            return "";
            //return ByteSize.FromBytes(TotalBytes).ToString();

            // 1 MB //if (TotalBytes >= 1073741824) //Giga Bytes
            //{
            // Decimal FileSize = Decimal.Divide(TotalBytes, 1073741824);
            // return String.Format("{0:##.#} GB", FileSize);
            //}
            //else if (TotalBytes >= 1048576) //Mega Bytes
            //{
            // Decimal FileSize = Decimal.Divide(TotalBytes, 1048576);
            // return String.Format("{0:##.#} MB", FileSize);
            //}
            //else if (TotalBytes >= 1024) //Kilo Bytes
            //{
            // Decimal FileSize = Decimal.Divide(TotalBytes, 1024);
            // return String.Format("{0:##.#} KB", FileSize);
            //}
            //else if (TotalBytes > 0)
            //{
            // Decimal FileSize = TotalBytes;
            // return String.Format("{0:##.#} Bytes", FileSize);
            //}
            //else
            //{
            // return "0 Bytes";
            //}

        }

        [UnitOfWork(IsDisabled = true)]
        private async Task<BFile> SaveFileHelper(int? tenantId, BFile bFile)
        {
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(tenantId))
                {
                    await _bFileRepository.InsertAsync(bFile);
                    await _unitOfWorkManager.Current.SaveChangesAsync();
                }
                await uow.CompleteAsync();
            }

            return bFile;
        }


        [UnitOfWork(IsDisabled = true)]
        public async Task Delete(int? tenantId, Guid fileId)
        {
            try
            {
                var bFile = await GetFileHelper(tenantId, fileId);

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
            catch (UserFriendlyException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }


        [UnitOfWork(IsDisabled = true)]
        public async Task<BFileDownloadOutput> DownLoad(int? tenantId, Guid id)
        {
            var bFile = await GetFileHelper(tenantId, id);

            BFileDownloadOutput galleryFile = null;

            if (bFile.FileStorage == FileStorage.AWS)
            {
                galleryFile = await _aws3BFileManager.Download(bFile.StorageFolder, bFile.FilePath, bFile.FileType);
            }
            else if (bFile.FileStorage == FileStorage.Local)
            {
                galleryFile = await _localBFileManager.Download(bFile.StorageFolder, bFile.FilePath, bFile.FileType);
            }
            else
            {
                throw new UserFriendlyException("FileNotFound");
            }

            galleryFile.FileName = bFile.DisplayName;

            return galleryFile;
        }

        [UnitOfWork(IsDisabled = true)]
        private async Task<BFile> GetFileHelper(int? tenantId, Guid id)
        {
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(tenantId))
                {
                    var bFile = await _bFileRepository.GetAll().AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);

                    if (bFile == null) throw new UserFriendlyException("RecordNotFound");

                    return bFile;
                }
            }
        }

    }
}
