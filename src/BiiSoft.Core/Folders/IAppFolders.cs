namespace BiiSoft.Folders
{
    public interface IAppFolders
    {
        string AssetsFolder { get; }
        string ImagesFolder { get; }
        string FontsFolder { get; }
        string UserProfilesFolder { get; }

        string AppLogsFolder { get; }
        string TemplateFolder { get; }
        string DownloadFolder { get; }
        string DownloadUrl { get; }
    }
}