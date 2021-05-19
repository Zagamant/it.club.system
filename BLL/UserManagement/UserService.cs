using System.BLL.Helpers;
using System.BLL.Models.RoleManagement;
using System.BLL.Models.UserManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace System.BLL.UserManagement
{
    /// <summary>
    /// Represent a user service
    /// </summary>
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        /// <summary>
        /// Initialize a new instance of the <see cref="UserService"/> class with specified <see cref="DataContext"/>.
        /// </summary>
        /// <param name="context">A <see cref="DataContext"/>.</param>
        /// <param name="userManager">A <see cref="UserManager{User}"/>.</param>
        /// <param name="signInManager">A <see cref="SignInManager{User}"/>.</param>
        /// <param name="mapper"></param>
        /// <param name="roleManager"></param>
        public UserService(DataContext context, UserManager<User> userManager, SignInManager<User> signInManager,
            IMapper mapper, RoleManager<Role> roleManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        /// <inheritdoc/>
        public async Task<UserModel> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                throw new AppException("User not found");

            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

            return result.Succeeded ? _mapper.Map<UserModel>(user) : null;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<int> GetIdAsync(User user)
        {
            var id = await _userManager.GetUserIdAsync(user);
            return Convert.ToInt32(id);
        }


        /// <inheritdoc/>
        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            //TODO: FIX ISSUE
            // var users = _userManager.Users.AsEnumerable().Select(async user =>
            // {
            //     var roles = await _userManager.GetRolesAsync(user);
            //     var userModel = _mapper.Map<UserModel>(user);
            //     userModel.Roles = roles;
            //     return userModel;
            // }).ToList();

            var users = await _userManager.Users.ToListAsync();
            var userModels = new List<UserModel>(users.Count);
            
            foreach (var user in users)
            {
                var newUser = _mapper.Map<UserModel>(user);
                newUser.Roles = await _userManager.GetRolesAsync(user);
                userModels.Add(newUser);
            }

            return userModels;
        }

        /// <inheritdoc/>
        public async Task<UserModel> GetAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            return user == null ? null : _mapper.Map<UserModel>(user);
        }

        public async Task<UserModel> GetAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null) throw new AppException("User not found");

            return _mapper.Map<UserModel>(user);
        }

        public async Task<UserModel> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) throw new AppException("User not found");

            return _mapper.Map<UserModel>(user);
        }

        /// <inheritdoc/>
        public async Task<UserModel> AddAsync(UserModel user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password)) throw new AppException("Password is required");

            if (_context.Users.Any(x => x.UserName == user.UserName))
                throw new AppException("Username \"" + user.UserName + "\" is already taken");

            var trueUser = _mapper.Map<User>(user);
            var result = await _userManager.CreateAsync(trueUser, password);

            await _context.SaveChangesAsync();

            return user;
        }

        /// <inheritdoc/>
        public async Task<UserModel> UpdateAsync(int id, UserModel newUser, string password = null)
        {
            var user = await _userManager.FindByIdAsync(newUser.Id.ToString());

            if (user == null)
                throw new AppException("User not found");

            user.Name = newUser.Name;
            user.MiddleName = newUser.MiddleName;
            user.Surname = newUser.Surname;
            user.BirthDay = newUser.BirthDay;
            user.AdditionalInfo = newUser.AdditionalInfo;
            user.UserName = newUser.UserName;
            user.Email = newUser.Email;
            user.PhoneNumber = newUser.PhoneNumber;

            if (!password.IsNullOrEmpty())
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);
            }

            user.Address ??= new Address
            {
                Country = newUser.Country,
                AddressLine = newUser.AddressLine,
                City = newUser.City
            };

            if (!user.Address.Country.IsNullOrEmpty() && user.Address.Country != newUser.Country)
            {
                user.Address.Country = newUser.Country;
            }

            if (!user.Address.City.IsNullOrEmpty() && user.Address.City != newUser.City)
            {
                user.Address.City = newUser.City;
            }

            if (!user.Address.AddressLine.IsNullOrEmpty() && user.Address.AddressLine != newUser.AddressLine)
            {
                user.Address.AddressLine = newUser.AddressLine;
            }

            //Roles management
            var oldRoles = await _userManager.GetRolesAsync(user);
            var rolesToAdd = newUser.Roles.Except(oldRoles);

            var rolesToRemove = oldRoles.Except(newUser.Roles);

            foreach (var roleToRemove in rolesToRemove)
            {
                await RemoveUsersRoleAsync(user.Id, roleToRemove);
            }

            foreach (var newRole in rolesToAdd)
            {
                await AddRoleToUserAsync(user.Id, newRole);
            }

            await _userManager.UpdateAsync(user: user);

            await _context.SaveChangesAsync();

            return _mapper.Map<UserModel>(user);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null) return;

            await _userManager.DeleteAsync(user);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task ConfirmEmailAsync(ConfirmEmailModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());

            if (user == null) throw new AppException($"User with id: {model.Id} not found");


            var result = await _userManager.ConfirmEmailAsync(user, model.Code);
            if (!result.Succeeded)
            {
                throw new AppException("Reset was failed");
            }
        }

        /// <inheritdoc/>
        public async Task<string> ForgotPasswordAsync(ForgotPasswordModel userModel)
        {
            var user = await _userManager.FindByEmailAsync(userModel.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // пользователь с данным email может отсутствовать в бд
                // тем не менее мы выводим стандартное сообщение, чтобы скрыть 
                // наличие или отсутствие пользователя в бд
                throw new AppException($"User with email: {userModel.Email} not found");
            }

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        /// <inheritdoc/>
        public async Task<string> GenerateConfirmationEmailAsync(ConfirmEmailModel userModel)
        {
            var user = await _userManager.FindByIdAsync(userModel.Id.ToString());
            if (user == null)
            {
                // пользователь с данным email может отсутствовать в бд
                // тем не менее мы выводим стандартное сообщение, чтобы скрыть 
                // наличие или отсутствие пользователя в бд
                throw new AppException($"User with email: {userModel.Email} not found");
            }

            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        /// <inheritdoc/>
        public async Task ResetPasswordAsync(ResetPasswordModel userModel)
        {
            var user = await _userManager.FindByEmailAsync(userModel.Email);
            if (user == null)
            {
                throw new AppException("Model not valid");
            }

            var result = await _userManager.ResetPasswordAsync(user, userModel.Code, userModel.Password);
            if (!result.Succeeded)
            {
                throw new AppException("Reset was failed");
            }
        }

        public async Task<IEnumerable<string>> GetRolesAsync(int userId)
        {
            var realUser = await _userManager.FindByIdAsync(userId.ToString());

            return await _userManager.GetRolesAsync(realUser);
        }

        public async Task<bool> AddRoleToUserAsync(int userId, string role)
        {
            var realUser = await _userManager.FindByIdAsync(userId.ToString());
            if (realUser == null) throw new ArgumentNullException($"User with Id: {userId} not found");

            var result = await _userManager.AddToRoleAsync(realUser, role);

            return result.Succeeded;
        }

        public async Task<bool> RemoveUsersRoleAsync(int userId, string role)
        {
            var realUser = await _userManager.FindByIdAsync(userId.ToString());
            if (realUser == null) throw new ArgumentNullException($"User with Id: {userId} not found");

            var result = await _userManager.RemoveFromRoleAsync(realUser, role);

            return result.Succeeded;
        }

        #region private helper methods

        /// <summary>
        /// AddAsync password hash and uniq salt for new user.
        /// </summary>
        /// <param name="password">Clear password.</param>
        /// <param name="passwordHash">Encrypted password.</param>
        /// <param name="passwordSalt">Password's salt.</param>
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        /// <summary>
        /// Verify is entered password equals hashed ones.
        /// </summary>
        /// <param name="password">Entered password.</param>
        /// <param name="storedHash">Hashed password.</param>
        /// <param name="storedSalt">Uniq salt that were used to encrypt password.</param>
        /// <returns>Is it corrected entered password.</returns>
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
            if (storedHash.Length != 64)
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", nameof(storedHash));
            if (storedSalt.Length != 128)
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).",
                    nameof(storedSalt));

            using var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return !computedHash.Where((t, i) => t != storedHash[i]).Any();
        }

        #endregion
    }
}