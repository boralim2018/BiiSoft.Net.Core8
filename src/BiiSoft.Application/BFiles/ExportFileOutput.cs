using System;
using System.ComponentModel.DataAnnotations;

namespace BiiSoft.BFiles
{
    public class ExportFileOutput
    {
        [Required]
        public string FileName { get; set; }
        [Required]
        public string FileToken { get; set; }
        public string FileUrl { get; set; }      
    }
}