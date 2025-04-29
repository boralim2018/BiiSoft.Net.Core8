using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Abp.UI;
using BiiSoft.Columns;
using BiiSoft.Enums;
using OfficeOpenXml;
using OfficeOpenXml.DataValidation;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace BiiSoft.Extensions
{
    public static class ExcelPackageExtensions
    {
        public static ExcelWorksheet CreateSheet(this ExcelWorkbook wb, string sheetName)
        {
            var ws = wb.Worksheets.Add(sheetName);
            ws.PrinterSettings.Orientation = eOrientation.Landscape;
            ws.PrinterSettings.FitToPage = true;
            //ws.PrinterSettings.PaperSize = ePaperSize.A3; //set default format paper size 
            ws.Cells.Style.Font.Size = BiiSoftConsts.DefaultFontSize; //Default font size for whole sheet
            ws.Cells.Style.Font.Name = BiiSoftConsts.DefaultFontName; //Default Font name for whole sheet
            ws.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            return ws;
        }

        public static ExcelWorksheet CreateSheet(this ExcelPackage p, string sheetName)
        {   
            return p.Workbook.CreateSheet(sheetName);
        }

        public static string GetString(this ExcelWorksheet sheet, int rowIndex, int columnIndex)
        {
            return sheet.Cells[rowIndex, columnIndex].Value?.ToString();
        }

        public static decimal? GetDecimalOrNull(this ExcelWorksheet sheet, int rowIndex, int columnIndex)
        {
            var value = sheet.Cells[rowIndex, columnIndex].Value;
            return value == null ? (decimal?)null : Convert.ToDecimal(value);
        }

        public static decimal GetDecimal(this ExcelWorksheet sheet, int rowIndex, int columnIndex)
        {
            var value = sheet.Cells[rowIndex, columnIndex].Value;
            return Convert.ToDecimal(value ?? 0);
        }

        public static long? GetLongOrNull(this ExcelWorksheet sheet, int rowIndex, int columnIndex)
        {
            var value = sheet.Cells[rowIndex, columnIndex].Value;
            return value == null ? (long?)null : Convert.ToInt64(value);
        }

        public static long GetLong(this ExcelWorksheet sheet, int rowIndex, int columnIndex)
        {
            var value = sheet.Cells[rowIndex, columnIndex].Value;
            return Convert.ToInt64(value ?? 0);
        }

        public static int? GetIntOrNull(this ExcelWorksheet sheet, int rowIndex, int columnIndex)
        {
            var value = sheet.Cells[rowIndex, columnIndex].Value;
            return value == null ? (int?)null : Convert.ToInt32(value);
        }

        public static int GetInt(this ExcelWorksheet sheet, int rowIndex, int columnIndex)
        {
            var value = sheet.Cells[rowIndex, columnIndex].Value;
            return Convert.ToInt32(value ?? 0);
        }

        public static bool? GetBoolOrNull(this ExcelWorksheet sheet, int rowIndex, int columnIndex)
        {
            var value = sheet.Cells[rowIndex, columnIndex].Value;
            return value == null ? (bool?)null : Convert.ToBoolean(value);
        }

        public static bool GetBool(this ExcelWorksheet sheet, int rowIndex, int columnIndex)
        {
            var value = sheet.Cells[rowIndex, columnIndex].Value;
            return Convert.ToBoolean(value ?? false);
        }

        public static DateTime? GetDateTimeOrNull(this ExcelWorksheet sheet, int rowIndex, int columnIndex)
        {
            var value = sheet.Cells[rowIndex, columnIndex].Value;
            return value == null ? (DateTime?)null : Convert.ToDateTime(value);
        }

        public static DateTime GetDateTime(this ExcelWorksheet sheet, int rowIndex, int columnIndex)
        {
            var value = sheet.Cells[rowIndex, columnIndex].Value;
            return Convert.ToDateTime(value ?? DateTime.MinValue);
        }

        public static string GetAddressName(this ExcelWorksheet sheet, int rowIndex, int columnIndex)
        {
            var address = sheet.Cells[rowIndex, columnIndex].Address;
            return address;
        }

        private static string FormatNumber(this int roundDigit)
        {
            var result = roundDigit < 1 || roundDigit > 8 ? "" : ".".PadRight(roundDigit + 1, '0');
            return $"#,##0{result}";
        }

        private static string FormatAccounting(this int roundDigit)
        {
            var result = roundDigit < 1 || roundDigit > 8 ? "00" : "".PadRight(roundDigit, '0');
            return $"#,##0.{result}_);[Red](#,##0.{result})";
        }

        private static string FormatPercentage(this int roundDigit)
        {
            return "#0\\.00%";
        }


        public static void AddFormula(
            this ExcelWorksheet sheet,
            int rowIndex,
            int columnIndex,
            string formula,
            int rounding = 2,
            CellFormat format = CellFormat.Accouting,            
            bool isBold = false)
        {
            var cell = sheet.Cells[rowIndex, columnIndex];

            cell.Formula = formula;
            cell.Style.Font.Bold = isBold;

            switch (format)
            {
                case CellFormat.Percentage:
                    cell.Style.Numberformat.Format = rounding.FormatPercentage();
                    break;
                case CellFormat.Number:
                    cell.Style.Numberformat.Format = rounding.FormatNumber();
                    break;
                case CellFormat.Accouting:
                    cell.Style.Numberformat.Format = rounding.FormatAccounting();
                    break;
                default:
                    cell.Style.Numberformat.Format = "";
                    break;
            }
        }

        public static void AddCheckbox(
            this ExcelWorksheet sheet,
            int rowIndex,
            int columnIndex,
            bool value,
            bool showCrosskForFalse = false)
        {
            int unicode = value ? 254 : showCrosskForFalse ? 120 : 168;
            char character = (char)unicode;
            sheet.Cells[rowIndex, columnIndex].Value = character.ToString();
            sheet.Cells[rowIndex, columnIndex].Style.Font.Name = "Wingdings";
            sheet.Cells[rowIndex, columnIndex].Style.Font.Size = 14;
        }

        public static void AddDateToCell(
            this ExcelWorksheet sheet,
            int rowIndex,
            int columnIndex,
            DateTime date,
            bool isBold = false)
        {
            var cell = sheet.Cells[rowIndex, columnIndex];
            cell.Value = date;
            cell.Style.Numberformat.Format = "yyyy-mm-dd";
            cell.Style.Font.Bold = isBold;
        }

        /// <summary>
        /// Time Format 12 hours => h:mm:ss AM/PM 24 hours => hh:mm:ss
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="date"></param>
        /// <param name="timeFormat"></param>
        /// <param name="isBold"></param>
        public static void AddDateTimeToCell(
            this ExcelWorksheet sheet,
            int rowIndex,
            int columnIndex,
            DateTime date,
            string timeFormat = "hh:mm:ss",
            bool isBold = false)
        {
            var cell = sheet.Cells[rowIndex, columnIndex];
            cell.Value = date;
            cell.Style.Numberformat.Format = $"yyyy-mm-dd {timeFormat}";
            cell.Style.Font.Bold = isBold;
        }

        public static void AddNumberToCell(
            this ExcelWorksheet sheet,
            int rowIndex,
            int columnIndex,
            decimal decimalNumber,
            int rounding = 2,
            CellFormat format = CellFormat.Number,           
            bool isBold = false,
            ExcelHorizontalAlignment textAlign = ExcelHorizontalAlignment.Right)
        {

            var cell = sheet.Cells[rowIndex, columnIndex];

            switch (format)
            {
                case CellFormat.Percentage:
                    cell.Style.Numberformat.Format = rounding.FormatPercentage();
                    break;
                case CellFormat.Number:                    
                    cell.Style.Numberformat.Format = rounding.FormatNumber();
                    break;
                case CellFormat.Accouting:
                    cell.Style.Numberformat.Format = rounding.FormatAccounting();
                    break;
                default:
                    cell.Style.Numberformat.Format = "";
                    break;
            }

            cell.Value = decimalNumber;
            cell.Style.Font.Bold = isBold;
            cell.Style.HorizontalAlignment = textAlign;
        }

        public static void MergeCell(
            this ExcelWorksheet sheet,
            int fromRowIndex,
            int fromColumnIndex,
            int toRowIndex,
            int toColumnIndex,
            ExcelHorizontalAlignment align)
        {
            var cellRage = sheet.Cells[fromRowIndex, fromColumnIndex, toRowIndex, toColumnIndex];
            cellRage.Merge = true;
            cellRage.Style.HorizontalAlignment = align;
        }

        public static void MergeCell(
            this ExcelWorksheet sheet,
            int fromRowIndex,
            int fromColumnIndex,
            int toRowIndex,
            int toColumnIndex,
            ExcelHorizontalAlignment align,
            ExcelVerticalAlignment vAlign)
        {
            var cellRage = sheet.Cells[fromRowIndex, fromColumnIndex, toRowIndex, toColumnIndex];
            cellRage.Merge = true;
            cellRage.Style.HorizontalAlignment = align;
            cellRage.Style.VerticalAlignment = vAlign;
        }

        public static void AddTextToCell(
            this ExcelWorksheet sheet,
            int rowIndex,
            int columnIndex,
            string text,
            bool isBold = false,
            bool wrapText = false,
            int indent = 0)
        {
            var cell = sheet.Cells[rowIndex, columnIndex];
            cell.Value = text;
            cell.Style.Font.Bold = isBold;
            cell.Style.WrapText = wrapText;
            cell.Style.Indent = indent;
        }

        public static string ToIndirectFormula(this string indirectCell)
        {
            return $"=IF(${indirectCell}=\"\",\"\",INDIRECT(SUBSTITUTE(${indirectCell},\" \",\"\")&\"Table[\"&${indirectCell}&\"]\"))";
        }

        public static void AddListValidation(
           this ExcelWorksheet sheet,
           ColumnOutput col,
           int fromRowIndex,
           int toRowIndex,
           int columnIndex)
        {
            if(col.ColumnType != ColumnType.Lookup) throw new UserFriendlyException("Column type is not Lookup!");
            if(col.LookupList.IsNullOrEmpty()) throw new UserFriendlyException("LookupList is required!");
            if (col.ColumnName.IsNullOrEmpty()) throw new UserFriendlyException("ColumnName is required");

            if (toRowIndex <= fromRowIndex) toRowIndex = fromRowIndex + 1;

            ExcelWorksheet lookupSheet = sheet.Workbook.CreateSheet(col.ColumnName);

            var rowIndex = 1;
            foreach (var value in col.LookupList)
            {
                col.Write(lookupSheet, rowIndex + 1, 1, value);
                rowIndex++;
            }

            var lookupColumn = new ColumnOutput { ColumnTitle = col.ColumnTitle, Width = col.Width };

            ExcelTable lookupTable = lookupSheet.InsertTable(new List<ColumnOutput> { lookupColumn }, $"{lookupSheet.Name}Table", 1, 1, rowIndex);
            ExcelRange validationRange = sheet.Cells[
                fromRowIndex + 1, // Start below header
                columnIndex,
                toRowIndex,
                columnIndex
            ];
            var validation = sheet.DataValidations.AddListValidation(validationRange.Address);
            validation.ShowErrorMessage = true;
            validation.ErrorStyle = ExcelDataValidationWarningStyle.warning;
            validation.ErrorTitle = "Invalid Value";
            validation.Error = "Please select a value from the list.";
            validation.ShowInputMessage = true;
            validation.PromptTitle = $"{col.ColumnTitle} Selection";
            validation.Prompt = $"Choose {col.ColumnTitle} from the dropdown.";

            //validation.Formula.ExcelFormula = $"'{lookupSheet.Name}'!{lookupTable.Address}";
            var formula = $"=INDIRECT(\"{lookupTable.Name}[{lookupTable.Columns[0].Name}]\")";
            validation.Formula.ExcelFormula = formula;
        }

        public static void AddIndirectListValidation(
           this ExcelTable table,
           ColumnOutput col,
           int columnIndex)
        {
            if (col.ColumnType != ColumnType.IndirectLookup) throw new UserFriendlyException("Column type is not Indirect Lookup!");
            if (col.LookupList.IsNullOrEmpty()) throw new UserFriendlyException("LookupList is required");
            if (col.IndirectIndex <= 0) throw new UserFriendlyException("IndirectIndex is required");
            if (col.ColumnName.IsNullOrEmpty()) throw new UserFriendlyException("ColumnName is required");

            ExcelWorksheet lookupSheet = table.WorkSheet.Workbook.CreateSheet(col.ColumnName);

            var colIndex = 1;
            foreach (var value in col.LookupList)
            {   
                var lookupItem = value.ToIndirectKeyValues();

                var rowIndex = 2;

                foreach (var item in lookupItem.Value)
                {
                    col.Write(lookupSheet, rowIndex, colIndex, item);
                    rowIndex++;
                }
              
                var lookupColumn = new ColumnOutput { ColumnTitle = lookupItem.Key, Width = col.Width };

                ExcelTable lookupTable = lookupSheet.InsertTable(new List<ColumnOutput> { lookupColumn }, $"{lookupItem.Key.NoSpaces()}Table", 1, colIndex, rowIndex - 1);
                
                colIndex++;
            }

            ExcelRange validationRange = table.WorkSheet.Cells[
                table.Address.Start.Row + 1, // Start below header
                columnIndex,
                table.Address.End.Row,
                columnIndex
            ];
            var validation = table.WorkSheet.DataValidations.AddListValidation(validationRange.Address);
            validation.ShowErrorMessage = true;
            validation.ErrorStyle = ExcelDataValidationWarningStyle.warning;
            validation.ErrorTitle = "Invalid Value";
            validation.Error = "Please select a value from the list.";
            validation.ShowInputMessage = true;
            validation.PromptTitle = $"{col.ColumnTitle} Selection";
            validation.Prompt = $"Choose {col.ColumnTitle} from the dropdown.";

            var indirectCell = table.WorkSheet.Cells[2, col.IndirectIndex].Address;

            //validation.Formula.ExcelFormula = $"'{lookupSheet.Name}'!{lookupTable.Address}";
            //var formula = $"=INDIRECT(\"{lookupTable.Name}[{lookupTable.Columns[0].Name}]\")";
            var formula = indirectCell.ToIndirectFormula();
            validation.Formula.ExcelFormula = formula;
        }

        public static ExcelTable InsertTable(
            this ExcelWorksheet sheet,
            List<ColumnOutput> columns,
            string tableName,
            int fromRowIndex,
            int fromColumnIndex,
            int toRowIndex,
            TableStyles style = TableStyles.Medium13)
        {
            if (toRowIndex <= fromRowIndex) toRowIndex = fromRowIndex + 1;

            var fromCell = sheet.GetAddressName(fromRowIndex, fromColumnIndex);
            var toCell = sheet.GetAddressName(toRowIndex, fromColumnIndex + columns.Count - 1);

            using (ExcelRange Rng = sheet.Cells[$"{fromCell}:{toCell}"])
            {
                //Indirectly access ExcelTableCollection class  
                ExcelTable table = sheet.Tables.Add(Rng, tableName);

                //Directly access ExcelTableCollection class  
                //ExcelTableCollection tblcollection = wsSheet1.Tables;  
                //ExcelTable table1 = tblcollection.Add(Rng, "tblSalesman");

                //Column Header
                var colIndex = 0;                
                foreach (var col in columns)
                {
                    table.AddUniqueColumn(col, colIndex);
                   
                    if (col.Width > 0) sheet.Column(fromColumnIndex + colIndex).Width = col.Width.PixcelToInches();
                    
                    if(col.ColumnType == ColumnType.Lookup && !col.LookupList.IsNullOrEmpty())
                    {
                        sheet.AddListValidation(col, fromRowIndex, toRowIndex, fromColumnIndex + colIndex);
                    }
                    else if(col.ColumnType == ColumnType.IndirectLookup && !col.LookupList.IsNullOrEmpty())
                    {
                        table.AddIndirectListValidation(col, fromColumnIndex + colIndex);
                    }

                    colIndex++;
                }

                table.ShowHeader = true;  
                table.ShowFilter = true;
                //table.ShowTotal = true;  

                //set table style
                table.TableStyle = style;

                return table;
            }
        }

        public static ExcelTable InsertTable(
            this ExcelWorksheet sheet,
            List<SummaryColumnOutput> columns,
            string tableName,
            int fromRowIndex,
            int fromColumnIndex,
            int toRowIndex,
            string totalLabel = "Total",
            TableStyles style = TableStyles.Medium13)
        {
            if (fromRowIndex >= toRowIndex) toRowIndex = fromRowIndex + 1;

            var fromCell = sheet.GetAddressName(fromRowIndex, fromColumnIndex);
            var toCell = sheet.GetAddressName(toRowIndex, fromColumnIndex + columns.Count - 1);

            using (ExcelRange Rng = sheet.Cells[$"{fromCell}:{toCell}"])
            {
                //Indirectly access ExcelTableCollection class  
                ExcelTable table = sheet.Tables.Add(Rng, tableName);

                //Directly access ExcelTableCollection class  
                //ExcelTableCollection tblcollection = wsSheet1.Tables;  
                //ExcelTable table1 = tblcollection.Add(Rng, "tblSalesman");

                //Column Header
                var colIndex = 0;
                foreach (var col in columns)
                {
                    var column = table.AddUniqueColumn(col, colIndex);

                    if (col.Width > 0) sheet.Column(fromColumnIndex + colIndex).Width = col.Width.PixcelToInches();

                    if (colIndex == 0 && !totalLabel.IsNullOrWhiteSpace())
                    {
                        column.TotalsRowLabel = totalLabel;
                    }
                    else if (col.SelectedFunction == RowFunctions.Custom)
                    {
                       if(!col.CustomFunction.IsNullOrWhiteSpace()) column.TotalsRowFormula = col.CustomFunction;
                    }
                    else if(col.SelectedFunction != RowFunctions.None)
                    {
                        column.TotalsRowFunction = col.SelectedFunction;
                    }

                    if (col.ColumnType == ColumnType.Lookup && !col.LookupList.IsNullOrEmpty())
                    {
                        sheet.AddListValidation(col, fromRowIndex, toRowIndex, fromColumnIndex + colIndex);
                    }
                    else if (col.ColumnType == ColumnType.IndirectLookup && !col.LookupList.IsNullOrEmpty())
                    {
                        table.AddIndirectListValidation(col, fromColumnIndex + colIndex);
                    }

                    colIndex++;
                }

                table.ShowHeader = true;  
                table.ShowFilter = true;
                table.ShowTotal = columns.Any(s => s.SelectedFunction != RowFunctions.None);

                //set table style
                table.TableStyle = style;

                return table;
            }
        }

        private static ExcelTableColumn AddUniqueColumn(this ExcelTable table, ColumnOutput col, int colIndex)
        {
            var column = table.Columns[colIndex];
            var find = table.Columns.Any(s => s.Name.ToLower() == col.ColumnTitle.ToLower());
            column.Name = (find ? $"{col.ColumnTitle} {colIndex + 1}" : col.ColumnTitle) + (col.IsRequired ? $" *" : "");
            return column;
        }

    }
}
