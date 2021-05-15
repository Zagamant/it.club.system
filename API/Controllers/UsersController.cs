using System.API.Helpers;
using System.BLL.EmailManagement;
using System.BLL.Models.RoleManagement;
using System.BLL.Models.UserManagement;
using System.BLL.UserManagement;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace System.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppSettings _appSettings;

        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public UsersController(
            IUserService userService,
            IOptions<AppSettings> appSettings, IEmailService emailService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
        }

        [AllowAnonymous]
        //[Authorize(Roles = "main_admin,admin")]
        [HttpPost]
        public async Task<ActionResult<UserModel>> Register([FromBody] UserRegister model) =>
            Ok(await _userService.AddAsync(model, model.Password));
        
        
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthenticate model)
        {
            var user = await _userService.AuthenticateAsync(model.Username, model.Password);

            if (user == null)
                return BadRequest(new {message = "Username or password is incorrect"});

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                user.Id,
                UserName = user.UserName,
                Name = user.Name,
                Surname = user.Surname,
                Token = tokenString
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await _userService.LogoutAsync();
            return Ok();
        }


        //[Authorize(Roles = "main_admin,admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAll()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserModel>> GetById(int id)
        {
            var user = await _userService.GetAsync(id);
            return Ok(user);
        }

        
        [Authorize(Roles = "main_admin,admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserModel model, string password = null) =>
            Ok(await _userService.UpdateAsync(id, model, password));

        
        //[Authorize(Roles = "main_admin,admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            return Ok();
        }


        [HttpPost("forgotpassword")]
        [AllowAnonymous]
        public async Task ForgotPassword(ForgotPasswordModel model)
        {
            var user = await _userService.GetByEmailAsync(model.Email);
            var code = await _userService.ForgotPasswordAsync(model);
            var callbackUrl = Url.Action("ResetPassword", "Users",
                new {userId = user.Id, code},
                HttpContext.Request.Scheme);

            await _emailService.SendEmailAsync(model.Email, "Reset Password",
                $"To reset password follow link: <a href='{callbackUrl}'>link</a>");
        }

        
        [HttpPost("SendEmailConfirmation")]
        public async Task SendEmailConfirmation()
        {
            var userId = Convert.ToInt32(HttpContext.User.Identity.Name);

            var user = await _userService.GetAsync(userId);

            var model = new ConfirmEmailModel
            {
                Email = user.Email,
                Id = userId
            };

            var code = await _userService.GenerateConfirmationEmailAsync(model);

            var callbackUrl = Url.Action("ConfirmEmail", "Users",
                new {user.Id, user.Email, Code = code},
                HttpContext.Request.Scheme);

            await _emailService.SendEmailAsync(model.Email, "Confirm Email",
                $"To confirm email follow link: <a href='{callbackUrl}'>link</a>");
        }

        
        [HttpGet("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailModel model)
        {
            await _userService.ConfirmEmailAsync(model);
            return Ok();
        }


        [HttpGet("ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromQuery] ResetPasswordModel model)
        {
            await _userService.ResetPasswordAsync(model);

            return Ok();
        }

        [Authorize(Roles = "main_admin")]
        [HttpPost("{id}/AddRole")]
        public async Task<bool> AddRoleToUser(int id, RoleModel role)
        {
            return await _userService.AddRoleToUser(id, role.Name);
        }

        [Authorize(Roles = "main_admin")]
        [HttpPost("{id}/RemoveRole")]
        public async Task<bool> RemoveRoleFromUser(int id, RoleModel role)
        {
            return await _userService.RemoveUsersRole(id, role.Name);
        }
        
        [Authorize(Roles = "main_admin,admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UploadPhoto(int id, [FromBody] UserModel model, string password = null) =>
            Ok(await _userService.UpdateAsync(id, model, password));

        
    }
}