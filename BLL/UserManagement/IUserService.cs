﻿using System.BLL.Models.UserManagement;
using System.Collections.Generic;
using System.DAL.Entities;
using System.Threading.Tasks;

namespace System.BLL.UserManagement
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
        Task<User> AuthenticateAsync(string username, string password);

        Task LogoutAsync();

        Task<int> GetAsync(User user);

        /// <summary>
        /// GetAsync all users from database.
        /// </summary>
        /// <returns>List of <see cref="User"/>s.</returns>
        Task<IEnumerable<User>> GetAllAsync();

        /// <summary>
        /// GetAsync 1 user from database by id.
        /// </summary>
        /// <param name="id">A user identifier.</param>
        /// <returns></returns>
        Task<User> GetAsync(int id);

        /// <summary>
        /// GetAsync 1 user from database by username.
        /// </summary>
        /// <param name="username">A user username.</param>
        /// <returns></returns>
        Task<User> GetAsync(string username);

        /// <summary>
        /// GetAsync 1 user from database by username.
        /// </summary>
        /// <param name="username">A user username.</param>
        /// <returns></returns>
        Task<User> GetByEmailAsync(string email);

        /// <summary>
        /// AddAsync new user and return it back
        /// </summary>
        /// <param name="user">A <see cref="User"/>.</param>
        /// <param name="password">Password</param>
        /// <returns>A <see cref="User"/></returns>
        Task<User> AddAsync(User user, string password);

        /// <summary>
        /// UpdateAsync existed user.
        /// </summary>
        /// <param name="user">A <see cref="User"/>.</param>
        /// <param name="password">Passwords</param>
        Task UpdateAsync(User user, string password = null);

        /// <summary>
        /// DeleteAsync existed user by id.
        /// </summary>
        /// <param name="id">A user identifier.</param>
        Task DeleteAsync(int id);

        Task ConfirmEmailAsync(ConfirmEmailModel model);

        Task<string> GenerateConfirmationEmailAsync(ConfirmEmailModel userModel);

        Task<string> ForgotPasswordAsync(ForgotPasswordModel userModel);

        Task ResetPasswordAsync(ResetPasswordModel userModel);
    }
}