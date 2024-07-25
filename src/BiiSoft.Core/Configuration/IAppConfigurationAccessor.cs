using Microsoft.Extensions.Configuration;

namespace BiiSoft.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
