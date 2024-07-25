using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace BiiSoft.Localization
{
    public static class BiiSoftLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(BiiSoftConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(BiiSoftLocalizationConfigurer).GetAssembly(),
                        "BiiSoft.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
