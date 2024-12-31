using BiiSoft.Columns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BiiSoft.BFiles.Dto
{
    public class ExportFileOutput
    {
        [Required]
        public string FileName { get; set; }
        [Required]
        public string FileToken { get; set; }
        public string FileUrl { get; set; }
    }

    public class ExportFileInput
    {
        [Required]
        public string FileName { get; set; }

        public List<ColumnOutput> Columns { get; set; }
    }

    public class ExportDataFileInput : ExportFileInput
    {
        public IReadOnlyList<object> Items { get; set; }
    }

}