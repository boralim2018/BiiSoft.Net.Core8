using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using BiiSoft.Authorization.Users;
using BiiSoft.MultiTenancy;
using Abp.Dependency;
using System.Globalization;
using Abp.Localization;
using BiiSoft.Entities;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using OfficeOpenXml;
using BiiSoft.Columns;
using System.Linq;
using Abp.Extensions;
using BiiSoft.Extensions;
using BiiSoft.Folders;
using BiiSoft.FileStorages;
using BiiSoft.BFiles.Dto;

namespace BiiSoft
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class BiiSoftAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        private readonly IApplicationLanguageManager _applicationLanguageManager;

        //protected readonly IConfigurationRoot _appConfig;

        protected BiiSoftAppServiceBase()
        {
            LocalizationSourceName = BiiSoftConsts.LocalizationSourceName;

            //var env = IocManager.Instance.Resolve<IWebHostEnvironment>();
            //_appConfig = env.GetAppConfiguration();
            //SERVER_ROOT = env.WebRootPath.Replace("\\", "/").EnsureEndsWith('/');

            //var httpContextAccessor = IocManager.Instance.Resolve<IHttpContextAccessor>();
            //BASE_URL = httpContextAccessor.HttpContext.Request.Host.Value.EnsureEndsWith('/');

            _applicationLanguageManager = IocManager.Instance.Resolve<ApplicationLanguageManager>();       
        }

        protected async Task<bool> IsDefaultLagnuageAsync()
        {            
            var defaultLanguage = await _applicationLanguageManager.GetDefaultLanguageOrNullAsync(AbpSession.TenantId);
            return defaultLanguage != null && defaultLanguage.Name == CultureInfo.CurrentCulture.Name;
        }

        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            using (CurrentUnitOfWork.SetTenantId(null))
            {
                return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
            }
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        protected TEntity MapEntity<TEntity, TPrimaryKey>(object input)
        {
            var entity = ObjectMapper.Map<TEntity>(input);

            if (entity is IUserEntity<TPrimaryKey>)
            {
                (entity as IUserEntity<TPrimaryKey>).UserId = AbpSession.UserId;
            }
            else if(entity is ICreationAudited)
            {
                (entity as ICreationAudited).CreatorUserId = AbpSession.UserId;
            }

            if (entity is IModificationAudited) 
            { 
                (entity as IModificationAudited).LastModifierUserId = AbpSession.UserId;
            }

            if(entity is IMayHaveTenant || entity is IMayHaveTenant)
            {
                (entity as IMayHaveTenant).TenantId = AbpSession.TenantId;
            }

            return entity;
        }

    }

    /// <summary>
    /// Base Excel class provide a method to export data into excel table
    /// </summary>
    public abstract class BiiSoftExcelAppServiceBase : BiiSoftAppServiceBase
    {
        protected readonly IFileStorageManager _fileStorageManager;
        protected readonly IAppFolders _appFolders; 
        protected BiiSoftExcelAppServiceBase() 
        {
            _fileStorageManager = IocManager.Instance.Resolve<IFileStorageManager>();
            _appFolders = IocManager.Instance.Resolve<IAppFolders>();
        }

        protected async Task<ExportFileOutput> ExportExcelAsync(ExportFileInput input)
        {
            var result = new ExportFileOutput
            {
                FileName = input.FileName,
                FileToken = $"{Guid.NewGuid()}.xlsx"
            };

            using (var p = new ExcelPackage())
            {
                var ws = p.CreateSheet(result.FileName.RemoveExtension());

                #region Row 1 Header Table
                int rowTableHeader = 1;
                //int colHeaderTable = 1;

                // write header collumn table
                var displayColumns = input.Columns.Where(s => s.Visible).OrderBy(s => s.Index).ToList();

                //foreach (var i in displayColumns)
                //{
                //    ws.AddTextToCell(rowTableHeader, colHeaderTable, i.ColumnTitle, true);
                //    if (i.Width > 0) ws.Column(colHeaderTable).Width = i.Width.PixcelToInches();

                //    colHeaderTable += 1;
                //}
                #endregion Row 1

                var rowIndex = rowTableHeader + 1;
                foreach (var row in input.Items)
                {
                    var colIndex = 1;
                    foreach (var col in displayColumns)
                    {
                        var value = row.GetType().GetProperty(col.ColumnName.ToPascalCase()).GetValue(row);

                        //if (col.ColumnName == "CreatorUserName")
                        //{
                        //    var newValue = value;

                        //    var creationTime = row.GetType().GetProperty("CreationTime")?.GetValue(row);

                        //    if (creationTime != null) newValue += $"\r\n{Convert.ToDateTime(creationTime).ToString("yyyy-MM-dd HH:mm:ss")}";

                        //    col.WriteCell(ws, rowIndex, colIndex, newValue);
                        //}
                        //else if (col.ColumnName == "LastModifierUserName")
                        //{
                        //    var newValue = value;

                        //    var modificationTime = row.GetType().GetProperty("LastModificationTime").GetValue(row);
                        //    if (modificationTime != null) newValue += $"\r\n{Convert.ToDateTime(modificationTime).ToString("yyyy-MM-dd HH:mm:ss")}";

                        //    col.WriteCell(ws, rowIndex, colIndex, newValue);
                        //}
                        //else
                        //{
                        //    col.WriteCell(ws, rowIndex, colIndex, value);
                        //}

                        col.WriteCell(ws, rowIndex, colIndex, value);

                        colIndex++;
                    }
                    rowIndex++;
                }

                ws.InsertTable(displayColumns, $"{ws.Name}Table", rowTableHeader, 1, rowIndex - 1);

                result.FileUrl = $"{_appFolders.DownloadUrl}?fileName={result.FileName}&fileToken={result.FileToken}";

                await _fileStorageManager.UploadTempFile(result.FileToken, p);
            }

            return result;

        }
    }

}
