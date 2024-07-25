using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace BiiSoft.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
