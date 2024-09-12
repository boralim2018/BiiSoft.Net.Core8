using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BiiSoft.BFiles.Dto
{
    public class BFileDownloadOutput
    {
        public Stream Stream { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
