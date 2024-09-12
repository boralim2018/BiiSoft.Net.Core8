using Abp.Dependency;
using Abp.Domain.Uow;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using BiiSoft.Enums;
using Abp.UI;
using BiiSoft.Extensions;
using BiiSoft.BFiles.Dto;

namespace BiiSoft.BFiles
{
    public class LocalBFileManager : BFileManagerBase, ILocalBFileManager, ITransientDependency
    {
        public LocalBFileManager()
        {
        }

        public async Task<BFile> Upload(int? tenatId, long curentUserId, UploadSource uploadSource, IFormFile file, string displayName)
        {

            var path = BuildPath(tenatId, file);
            var folderToSave = Path.Combine(BiiSoftConsts.ResourcesFolder, path.FolderName);
            Directory.CreateDirectory(folderToSave);
                        
            var fullPath = Path.Combine(BiiSoftConsts.ResourcesFolder, path.FilePath);            

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var gallery = BFile.Create(
                tenatId, 
                curentUserId,
                path.FileName,
                displayName,               
                path.StarageName,
                path.FilePath,
                file.ContentType,
                Path.GetExtension(file.FileName),
                FileStorage.Local,
                uploadSource);
         
            return gallery;
            
        }

        public async Task<BFile> UploadImage(int? tenatId, long curentUserId, UploadSource uploadSource, IFormFile file, string displayName, int resizeMaxWidth)
        {
            var imageExtension = Path.GetExtension(file.FileName).Replace(".","").ToLowerInvariant();
            if (!BiiSoftConsts.ImageMineTypes.ContainsKey(imageExtension)) throw new UserFriendlyException("InvalidImage");

            var path = BuildPath(tenatId, file);
            var folderToSave = Path.Combine(BiiSoftConsts.ResourcesFolder, path.FolderName);
            Directory.CreateDirectory(folderToSave);
            var fullPath = Path.Combine(BiiSoftConsts.ResourcesFolder, path.FilePath);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var gallery = BFile.Create(
                tenatId,
                curentUserId,
                path.FileName,
                displayName,
                BiiSoftConsts.ResourcesFolder,
                path.FilePath,
                file.ContentType,
                imageExtension,
                FileStorage.Local,
                uploadSource);

            return gallery;

        }

        public async Task<BFileDownloadOutput> Download(string mainFolderName, string storageFilePath, string contentType)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), mainFolderName, storageFilePath);

            if (!File.Exists(fullPath)) return null;
            
            var memory = new MemoryStream();

            using (var stream = new FileStream(fullPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0; 
            
            return new BFileDownloadOutput()
            {
                Stream = memory,
                ContentType = contentType
            };
        }

        public async Task<bool> Delete(string mainFolderName, string storageFilePath)
        {
            return await Task.Run(() => {
                try
                {
                    var path = Path.Combine(mainFolderName, storageFilePath);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    return true;
                }
                catch(Exception ex) 
                {
                    throw new UserFriendlyException(ex.Message);
                }
            });            
        }

    }
}
