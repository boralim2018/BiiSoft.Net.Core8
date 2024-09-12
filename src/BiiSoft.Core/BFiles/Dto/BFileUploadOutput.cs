using Abp.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiiSoft.BFiles.Dto
{
    public class BFileUploadOutput
    {
        public string FileName { get; set; }

        public string FileType { get; set; }

        public Guid Id { get; set; }

    }
}
