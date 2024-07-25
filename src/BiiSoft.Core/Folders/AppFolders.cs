using Abp.Dependency;

namespace BiiSoft.Folders
{
    public class AppFolders : IAppFolders, ISingletonDependency
    {
        public string AssetsFolder { get; set; }
        public string ImagesFolder { get; set; }
        public string FontsFolder { get; set; }
        public string UserProfilesFolder { get; set; }

        public string AppLogsFolder { get; set; }
        public string TemplateFolder { get; set; }
        public string DownloadFolder { get; set; }
        public string DownloadUrl { get; set; }
    }
}