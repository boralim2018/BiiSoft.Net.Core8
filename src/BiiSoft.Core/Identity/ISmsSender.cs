using System.Threading.Tasks;

namespace BiiSoft.Identity
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}