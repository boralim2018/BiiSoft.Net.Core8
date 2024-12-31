using BiiSoft.FileStorages;
using System;
using System.Threading.Tasks;
using BiiSoft.Extensions;
using BiiSoft.Columns;
using OfficeOpenXml;
using BiiSoft.Folders;
using BiiSoft.BFiles.Dto;
using System.Linq;
using Abp.Extensions;

namespace BiiSoft.Excels
{
    public class ExcelManager : BiiSoftDomainServiceBase, IExcelManager
    {
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IAppFolders _appFolders;
        public ExcelManager(
            IAppFolders appFolders,
            IFileStorageManager fileStorageManager) 
        {
            _fileStorageManager = fileStorageManager;
            _appFolders = appFolders;
        }

        public async Task<ExportFileOutput> ExportExcelTemplateAsync(ExportFileInput input)
        {
            var result = new ExportFileOutput
            {
                FileName = input.FileName,
                FileToken = $"{Guid.NewGuid()}.xlsx"
            };

            using (var p = new ExcelPackage())
            {
                var ws = p.CreateSheet(result.FileName.RemoveExtension());

                ws.InsertTable(input.Columns, $"{ws.Name}Table", 1, 1, 5);

                result.FileUrl = $"{_appFolders.DownloadUrl}?fileName={result.FileName}&fileToken={result.FileToken}";

                await _fileStorageManager.UploadTempFile(result.FileToken, p);
            }

            return result;
        }

        public async Task<ExportFileOutput> ExportExcelAsync(ExportDataFileInput input)
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
