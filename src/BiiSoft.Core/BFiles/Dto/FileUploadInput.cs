using BiiSoft.Enums;

namespace BiiSoft.BFiles.Dto
{
    public class FileUploadInput
    {
        public string DisplayName { get; set; }
        public UploadSource UploadSource { get; set; }
    }
}
