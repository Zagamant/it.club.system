using System.BLL.Models.UserManagement;
using System.Threading.Tasks;

namespace BlazorClient.Services.AuthenticationManagement
{
    public interface IAuthenticationService
    {
        UserModel User { get; }
        Task Initialize();
        Task Login(string username, string password);
        Task Logout();
    }
}