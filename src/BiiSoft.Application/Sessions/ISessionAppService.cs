using System.Threading.Tasks;
using Abp.Application.Services;
using BiiSoft.Sessions.Dto;

namespace BiiSoft.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
