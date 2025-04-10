using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BiiSoft.Enums
{
    public enum FileStorage
    {
        Local = 0,
        AWS = 1,
    }

    public enum UploadSource
    {
        Attachment = 0,
        CompanyLogo = 1,
        UserProfile = 2,
        Item = 3,
        FormTemplate = 4,
    }
}
