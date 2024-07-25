using Abp.Dependency;
using Abp.Domain.Uow;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using BiiSoft.Enums;
using Abp.UI;
using BiiSoft.Extensions;

namespace BiiSoft.BFiles
{
    public class LocalBFileManager : BFileManagerBase, ILocalBFileManager, ITransientDependency
    {
        public LocalBFileManager()
        {
        }

        [UnitOfWork(IsDisabled = true)]
        public async Task<BFile> Upload(int? tenatId, long curentUserId, UploadSource uploadSource, IFormFile file, string displayName)
        {

            var path = BuildPath(tenatId, file);

            //C://Zobookk/zobookk-resources/Documents/Tenant_1/2022_06
            //var folderToSave = Path.Combine(Directory.GetCurrentDirectory(), BiiSoftConsts.ResourcesFolder, path.FolderName);
            var folderToSave = Path.Combine(BiiSoftConsts.ResourcesFolder, path.FolderName);
            Directory.CreateDirectory(folderToSave);

            //C://Zobookk/zobookk-resources/Documents/Tenant_1/2022_06/0000-0000-0000-0000-0000.jpg
            //var fullPath = Path.Combine(Directory.GetCurrentDirectory(), BiiSoftConsts.ResourcesFolder, path.FilePath);            
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

        [UnitOfWork(IsDisabled = true)]
        public async Task<BFile> UploadImage(int? tenatId, long curentUserId, UploadSource uploadSource, IFormFile file, string displayName, int resizeMaxWidth)
        {
            var imageExtension = Path.GetExtension(file.FileName).Replace(".","").ToLowerInvariant();
            if (!BiiSoftConsts.ImageMineTypes.ContainsKey(imageExtension)) throw new UserFriendlyException("InvalidImage");

            //Stream saveStream = file.OpenReadStream();

            //if (SKEncodedImageFormatDic.ContainsKey(imageExtension))
            //{
            //    saveStream = saveStream.ToFixed(resizeMaxWidth, SKEncodedImageFormatDic[imageExtension]);
            //}

            var path = BuildPath(tenatId, file);

            //C://Zobookk/zobookk-resources/Documents/Tenant_1/2022_06
            //var folderToSave = Path.Combine(Directory.GetCurrentDirectory(), BiiSoftConsts.ResourcesFolder, path.FolderName);
            var folderToSave = Path.Combine(BiiSoftConsts.ResourcesFolder, path.FolderName);
            Directory.CreateDirectory(folderToSave);

            //C://Zobookk/zobookk-resources/Documents/Tenant_1/2022_06/0000-0000-0000-0000-0000.jpg
            //var fullPath = Path.Combine(Directory.GetCurrentDirectory(), BiiSoftConsts.ResourcesFolder, path.FilePath);            
            var fullPath = Path.Combine(BiiSoftConsts.ResourcesFolder, path.FilePath);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                //await saveStream.CopyToAsync(stream);
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

        [UnitOfWork(IsDisabled = true)]
        public async Task<BFileDownloadOutput> Download(string mainFolderName, string storageFilePath, string contentType)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), mainFolderName, storageFilePath);
            
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

        [UnitOfWork(IsDisabled = true)]
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
