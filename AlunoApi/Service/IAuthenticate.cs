using System.Threading.Tasks;

namespace AlunoApi.Service
{
    public interface IAuthenticate
    {

        Task<bool> Authenticate(string email, string password);
        Task<bool> RegisterUser(string email, string password);
        Task Logout();

    }
}
