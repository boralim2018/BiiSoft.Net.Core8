using Abp.Domain.Entities;
using Abp.Timing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BiiSoft.Enums;
using BiiSoft.Entities;

namespace BiiSoft.BFiles
{
    [Table("BiiFiles")]
    public class BFile : NameActiveEntity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }


        [MaxLength(BiiSoftConsts.MaxLengthLongName)]
        public string StorageFolder { get; private set; }

        [MaxLength(BiiSoftConsts.MaxLengthDescription)]
        public string FilePath { get; private set; }

        [Required]
        [MaxLength(BiiSoftConsts.MaxLengthLongName)]
        public string FileType { get; private set; }

        [Required]
        [MaxLength(BiiSoftConsts.MaxLengthCode)]
        public string FileExtension { get; private set; }

        [Required]
        public FileStorage FileStorage { get; private set; }
        [Required]
        public UploadSource UploadSource { get; private set; }

        public static BFile Create(
            int? tenantId,
            long userId,
            string name,
            string displayName,
            string storageFolder,
            string filePath,
            string fileType,
            string fileExtension,
            FileStorage fileStorage,
            UploadSource uploadSource
            )
        {
            return new BFile
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                CreatorUserId = userId,
                CreationTime = Clock.Now,
                Name = name,
                DisplayName = displayName,
                StorageFolder = storageFolder,
                FilePath = filePath,
                FileType = fileType,
                FileExtension = fileExtension,
                FileStorage = fileStorage,
                UploadSource = uploadSource,
                IsActive = true
            };
        }

        public void Update(
            long userId,
            string name,
            string displayName,
            string storageFolder,
            string filePath,
            string fileType,
            string fileExtension,
            FileStorage fileStorage,
            UploadSource uploadSource
            )
        {
            LastModifierUserId = userId;
            LastModificationTime = Clock.Now;
            Name = name;
            DisplayName = displayName;
            StorageFolder = storageFolder;
            FilePath = filePath;
            FileType = fileType;
            FileExtension = fileExtension;
            FileStorage = fileStorage;
            UploadSource = uploadSource;
        }
    }
}
