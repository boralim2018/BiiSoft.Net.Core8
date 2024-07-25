using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BiiSoft.Configuration.Dto;
using BiiSoft.Configuration.Host.Dto;
using BiiSoft.Dtos;

namespace BiiSoft.Configuration.User
{
    public interface IUserSettingsAppService : IApplicationService
    {
        Task UpdateUIEnable(bool input);
        //Task UpdateTheme(string name, string dark);
        //Task UpdateFontSize(int fontSize);
        //Task UpdateRipple(bool ripple);
        //Task UpdateInputStyle(string style);
        //Task UpdateMenuType(string type);
        //Task Reset();
    }
}
