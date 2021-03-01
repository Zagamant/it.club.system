using System.BLL.Models.UserManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorClient.Models;
using BlazorClient.Models.Account;

namespace BlazorClient.Services.AccountManagement
{
    public interface IAccountService
    {
        User User { get; }
        Task Initialize();
        Task<UserLoginData> Login(Login model);
        Task Logout();
        Task<UserModel> Register(AddUser model);
        Task<IEnumerable<UserModel>> GetAll();
        Task<UserModel> GetById(int id);
        Task<UserModel> Update(int id, UserModel model, string password = null);
        Task Delete(int id);
    }
}