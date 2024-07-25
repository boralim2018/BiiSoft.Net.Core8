using Abp.Configuration;
using BiiSoft.Debugging;
using System.Collections.Generic;

namespace BiiSoft
{
    public class BiiSoftConsts
    {
        public const string AbpApiClientUserAgent = "AbpApiClient";

        public const string LocalizationSourceName = "BiiSoft";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;

        public const string DefaultFontName = "Khmer OS Battambang";
        public const int DefaultFontSize = 12;

        public const string TokenValidityKey = "token_validity_key";
        public static string UserIdentifier = "user_identifier";
        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase = DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "4bbc61aac9c845af8e4ae53c7ca01a9a";

        public const int DefaultPageSize = 10;

        public const int MaxLengthCode = 16;
        public const int MaxLengthLongCode = 32;
        public const int MaxLengthName = 64;
        public const int MaxLengthLongName = 128;
        public const int MaxLengthDescription = 256;
        public const int MaxLengthLongDescription = 512;
        public const int MaxLengthNote = 1024;
        public const int MaxLengthLongNote = 5000;
        public const string MaxLengthCodeErrorMessage = "Cannot enter more than 16 characters!";
        public const string MaxLengthLongCodeErrorMessage = "Cannot enter more than 32 characters!";
        public const string MaxLengthNameErrorMessage = "Cannot enter more than 64 characters!";
        public const string MaxLengthLongNameErrorMessage = "Cannot enter more than 128 characters!";
        public const string MaxLengthDescriptionErrorMessage = "Cannot enter more than 256 characters!";
        public const string MaxLengthLongDescriptionErrorMessage = "Cannot enter more than 512 characters!";
        public const string MaxLengthNoteErrorMessage = "Cannot enter more than 1024 characters!";
        public const string MaxLengthLongNoteErrorMessage = "Cannot enter more than 5000 characters!";

        public const string DefaultAdminEmail = "boralim.corarl@gmail.com";
        public const string DefaultAdminPassword = "Admin@BiiSoft123";
        public const string DefaultPassword = "Pwd@123";

        public const int MaxFileSize = 10485760; //10MB limit for us
        public const int MaxProfilePictureWidth = 1024; 
        public const int MaxProfilePictureSize = 5242880; //5MB
        public const string BFilesFolder = "BFiles";
        public const string ResourcesFolder = "biisoft-resources";

        public const int LocationCodeLength = 11;

        #region Theme Setting

        public const bool UISettingEnable = false;
        public const string ThemeName = "lara-light-indigo";
        public const string ThemeColor = "light";
        public const bool Ripple = false;
        public const string InputStyle = "outlined";
        public const string MenuType = "static";
        public const int FontSize = 14;

        #endregion


        public static readonly Dictionary<string, string> FileMineTypes = new Dictionary<string, string>{ 
            //other file
            {"pdf", "application/pdf" },
            {"xls", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
            {"xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
            {"txt", "text/plain" },
            {"htm", "text/html" },
            {"html", "text/html" },
            {"stm", "text/html" },
        };

        public static readonly Dictionary<string, string> ImageMineTypes = new Dictionary<string, string>{ 
            //image file
            {"bmp","image/bmp"},
            {"cod","image/cis-cod"},
            {"gif","image/gif"},
            {"ief","image/ief"},
            {"jpe","image/jpeg"},
            {"jpeg","image/jpeg"},
            {"jpg","image/jpeg"},
            {"jfif","image/pipeg"},
            {"svg","image/svg+xml"},
            {"tif","image/tiff"},
            {"tiff","image/tiff"},
            {"ras","image/x-cmu-raster"},
            {"cmx","image/x-cmx"},
            {"ico","image/x-icon"},
            {"png","image/png"},
            {"pnm","image/x-portable-anymap"},
            {"pbm","image/x-portable-bitmap"},
            {"pgm","image/x-portable-graymap"},
            {"ppm","image/x-portable-pixmap"},
            {"rgb","image/x-rgb"},
            {"xbm","image/x-xbitmap"},
            {"xpm","image/x-xpixmap"},
            {"xwd","image/x-xwindowdump"},
            {"wbmp", "image/vnd.wap.wbmp" },
            {"webp", "image/webp" },
            {"kpm", "image/kpm" },
            {"ktx", "image/ktx" },
            {"astc", "image/astc" },
            {"dng", "image/dng" },
            {"heif", "image/heif" },
            {"avif", "image/Avif" },

        };


    }
}
