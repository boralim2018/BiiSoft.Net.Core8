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
        CompanyLogo = 0,
        UserProfile = 1,
        Item = 2,
        FormTemplate = 3
    }
}
