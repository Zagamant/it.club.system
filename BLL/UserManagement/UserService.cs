using System.BLL.Helpers;
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

namespace System.BLL.UserManagement
{
    /// <summary>
    /// Represent a user service
    /// </summary>
    public class UserService : IUserService
    {
        private DataContext _context;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initialize a new instance of the <see cref="UserService"/> class with specified <see cref="DataContext"/>.
        /// </summary>
        /// <param name="context">A <see cref="DataContext"/>.</param>
        /// <param name="userManager">A <see cref="UserManager{User}"/>.</param>
        /// <param name="signInManager">A <see cref="SignInManager{User}"/>.</param>
        public UserService(DataContext context, UserManager<User> userManager, SignInManager<User> signInManager,
            IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
            return await _userManager.Users.Select(user => _mapper.Map<UserModel>(user)).ToListAsync();
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
        public async Task<UserModel> UpdateAsync(int id, UserModel model, string password = null)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());

            if (user == null)
                throw new AppException("User not found");

            user.Name = model.Name;
            user.MiddleName = model.MiddleName;
            user.Surname = model.Surname;
            user.BirthDay = model.BirthDay;
            user.AdditionalInfo = model.AdditionalInfo;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            if (!password.IsNullOrEmpty())
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);
            }

            user.Address ??= new Address
            {
                Country = model.Country,
                AddressLine = model.AddressLine,
                City = model.City
            };

            if (!user.Address.Country.IsNullOrEmpty() && user.Address.Country != model.Country)
            {
                user.Address.Country = model.Country;
            }

            if (!user.Address.City.IsNullOrEmpty() && user.Address.City != model.City)
            {
                user.Address.City = model.City;
            }

            if (!user.Address.AddressLine.IsNullOrEmpty() && user.Address.AddressLine != model.AddressLine)
            {
                user.Address.AddressLine = model.AddressLine;
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