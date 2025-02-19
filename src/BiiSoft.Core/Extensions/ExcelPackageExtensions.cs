using Abp.Extensions;
using BiiSoft.Columns;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using BiiSoft.Enums;
using System.Linq;
using System.Drawing;
using Abp.Collections.Extensions;

namespace BiiSoft.Extensions
{
    public static class ExcelPackageExtensions
    {
        public static ExcelWorksheet CreateSheet(this ExcelPackage p, string sheetName)
        {
            var ws = p.Workbook.Worksheets.Add(sheetName);
            ws.PrinterSettings.Orientation = eOrientation.Landscape;
            ws.PrinterSettings.FitToPage = true;
            //ws.PrinterSettings.PaperSize = ePaperSize.A3; //set default format paper size 
            ws.Cells.Style.Font.Size = BiiSoftConsts.DefaultFontSize; //Default font size for whole sheet
            ws.Cells.Style.Font.Name = BiiSoftConsts.DefaultFontName; //Default Font name for whole sheet
            ws.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            return ws;
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

        public static void AddListValidation(
            this ExcelWorksheet sheet,
            int rowIndex,
            int columnIndex,
            List<string> list,
            string value)
        {
            var address = sheet.GetAddressName(rowIndex, columnIndex);
            var dataValidation = sheet.DataValidations.AddListValidation(address);
            foreach (var i in list)
            {
                dataValidation.Formula.Values.Add(i);
            }
            sheet.Cells[rowIndex, columnIndex].Value = value;
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

        private static void AddListValidation(
           this ExcelWorksheet sheet,
           int fromRowIndex,
           int toRowIndex,
           int columnIndex,
           List<string> list)
        {
            var from = sheet.GetAddressName(fromRowIndex, columnIndex);
            var to = sheet.GetAddressName(toRowIndex, columnIndex);
            ExcelRange colRng = sheet.Cells[$"{from}:{to}"];

            var dataValidation = sheet.DataValidations.AddListValidation(colRng.Address);
            foreach (var i in list)
            {
                dataValidation.Formula.Values.Add(i);
            }
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
                var colHash = new HashSet<string>();
                foreach (var col in columns)
                {
                    table.Columns[colIndex].Name = (colHash.Contains(col.ColumnTitle) ? $"{col.ColumnTitle}{colIndex + 1}" : col.ColumnTitle) + (col.IsRequired ? $"* " : "");
                    if (col.Width > 0) sheet.Column(fromColumnIndex + colIndex).Width = col.Width.PixcelToInches();
                    
                    if(col.ColumnType == ColumnType.Lookup && !col.LookupList.IsNullOrEmpty())
                    {
                        AddListValidation(sheet, fromRowIndex, toRowIndex, colIndex, col.LookupList);
                    }

                    colHash.Add(table.Columns[colIndex].Name);
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
                var colHash = new HashSet<string>();
                foreach (var col in columns)
                {
                    table.Columns[colIndex].Name = (colHash.Contains(col.ColumnTitle) ? $"{col.ColumnTitle}{colIndex + 1}" : col.ColumnTitle) + (col.IsRequired ? $"* " : "");
                    if (col.Width > 0) sheet.Column(fromColumnIndex + colIndex).Width = col.Width.PixcelToInches();

                    if (colIndex == 0 && !totalLabel.IsNullOrWhiteSpace())
                    {
                        table.Columns[0].TotalsRowLabel = totalLabel;
                    }
                    else if (col.SelectedFunction == RowFunctions.Custom)
                    {
                       if(!col.CustomFunction.IsNullOrWhiteSpace()) table.Columns[colIndex].TotalsRowFormula = col.CustomFunction;
                    }
                    else if(col.SelectedFunction != RowFunctions.None)
                    {
                        table.Columns[colIndex].TotalsRowFunction = col.SelectedFunction;
                    }

                    if (col.ColumnType == ColumnType.Lookup && !col.LookupList.IsNullOrEmpty())
                    {
                        AddListValidation(sheet, fromRowIndex, toRowIndex, colIndex, col.LookupList);
                    }

                    colHash.Add(table.Columns[colIndex].Name);
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

    }
}
