using System.BLL.Models.UserManagement;
using System.Collections.Generic;
using System.DAL.Entities;
using System.Threading.Tasks;

namespace BlazorClient.Services.UserManagement
{
    /// <summary>
    /// Represent user management service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// AuthenticateAsync user on server and create JWT-token.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>A <see cref="User"/></returns>
        Task<UserModel> AuthenticateAsync(string username, string password);

        Task LogoutAsync();

        /// <summary>
        /// GetIdAsync all users from database.
        /// </summary>
        /// <returns>List of <see cref="User"/>s.</returns>
        Task<IEnumerable<UserModel>> GetAllAsync();

        /// <summary>
        /// GetIdAsync 1 user from database by id.
        /// </summary>
        /// <param name="id">A user identifier.</param>
        /// <returns></returns>
        Task<UserModel> GetAsync(int id);

        /// <summary>
        /// AddAsync new user and return it back
        /// </summary>
        /// <param name="user">A <see cref="UserRegister"/>.</param>
        /// <returns>A <see cref="User"/></returns>
        Task<UserModel> AddAsync(UserRegister user);

        /// <summary>
        /// UpdateAsync existed user.
        /// </summary>
        /// <param name="model">A <see cref="User"/>.</param>
        /// <param name="password">Passwords</param>
        Task<UserModel> UpdateAsync(int id, UserModel model, string password = null);

        /// <summary>
        /// DeleteAsync existed user by id.
        /// </summary>
        /// <param name="id">A user identifier.</param>
        Task DeleteAsync(int id);

        Task ConfirmEmailAsync(ConfirmEmailModel model);

        Task ForgotPasswordAsync(ForgotPasswordModel userModel);
        
        Task<IEnumerable<string>> GetRolesAsync(int userId);
        Task<bool> AddRole(int userId, int roleId);
        Task<bool> RemoveRole(int userId, int roleId);
        
    }
}
