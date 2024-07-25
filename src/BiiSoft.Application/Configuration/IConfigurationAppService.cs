using System.Threading.Tasks;
using BiiSoft.Configuration.Dto;

namespace BiiSoft.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
