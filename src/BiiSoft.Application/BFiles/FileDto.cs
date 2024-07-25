using Abp.Domain.Entities;
using BiiSoft.Entities;
using BiiSoft.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.BFiles
{
    public class FileDto
    {
        public Guid Id { get; set; }
        public string StorageFolder { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public string FileExtension { get; set; }
        public FileStorage FileStorage { get; set; }
        public UploadSource UploadSource { get; set; }
    }
}
