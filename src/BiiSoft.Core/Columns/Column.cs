using BiiSoft.Enums;
using BiiSoft.Extensions;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Columns
{

    public enum ColumnType
    {
        Text = 1,
        DateTime = 2,
        Number = 3,
        Bool = 4,
        Date = 5,
        CheckBox = 6,
        WrapText = 7,
    }

    //SummaryFunction Custom
    //101 AVERAGE 
    //102 COUNT
    //103 COUNTA
    //104 MAX
    //105 MIN
    //106 PRODUCT
    //107 STDEV
    //108 STDEVP
    //109 SUM
    //110 VAR
    //111 VARP

    //public enum RowFunctions
    //{
    //    Average = 0,
    //    Count = 1,
    //    CountNumbers = 2,
    //    Custom = 3,
    //    Max = 4,
    //    Min = 5,
    //    None = 6,
    //    StdDev = 7,
    //    Sum = 8,
    //    Var = 9
    //}
    
    public class ColumnOutput
    {   
        public string ColumnName { get; set; }
        public string ColumnTitle { get; set; }
        public int Index { get; set; }
        public bool Visible { get; set; }
        public decimal Width { get; set; }
        public ColumnType ColumnType { get; set; }
        public CellFormat CellFormat { get; set; } = CellFormat.Number;
        public int RoundingDigits { get; set; } 
        public bool ShowCrossForFalse { get; set; }
        public bool IsRequired { get; set; }
    }

    public class SummaryColumnOutput : ColumnOutput
    {
        public List<RowFunctions> AvailableFunctions { get; set; }
        public RowFunctions SelectedFunction { get; set; } = RowFunctions.None;
        public string CustomFunction { get; set; }
    }

    public static class ColumnExtensions
    {
        public static void WriteCell(
            this ColumnOutput col, 
            ExcelWorksheet ws, 
            int rowIndex, 
            int colIndex, 
            object value)
        {
            switch (col.ColumnType)
            {
                case ColumnType.DateTime:
                    ws.AddDateTimeToCell(rowIndex, colIndex, Convert.ToDateTime(value));
                    break;
                case ColumnType.Date:
                    ws.AddDateToCell(rowIndex, colIndex, Convert.ToDateTime(value));
                    break;
                case ColumnType.Number:                   
                    ws.AddNumberToCell(rowIndex, colIndex, Convert.ToDecimal(value), col.RoundingDigits, col.CellFormat);
                    break;
                case ColumnType.Bool:
                    ws.AddTextToCell(rowIndex, colIndex, Convert.ToBoolean(value).ToString());
                    break;
                case ColumnType.CheckBox:                    
                    ws.AddCheckbox(rowIndex, colIndex, Convert.ToBoolean(value), col.ShowCrossForFalse);
                    break;
                default:
                    ws.AddTextToCell(rowIndex, colIndex, value?.ToString(), false, col.ColumnType == ColumnType.WrapText);
                    break;
            }
        }
    }
}
