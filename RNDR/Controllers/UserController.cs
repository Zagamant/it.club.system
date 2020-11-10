using System.API.Helpers;
using System.BLL.EmailManagement;
using System.BLL.Helpers;
using System.BLL.Models.UserManagement;
using System.BLL.UserManagement;
using System.Collections.Generic;
using System.DAL.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace System.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class UsersController : ControllerBase
	{

		private readonly IEmailService _emailService;
		private readonly IUserService _userService;
		private readonly IMapper _mapper;
		private readonly AppSettings _appSettings;

		public UsersController(
			IUserService userService,
			IMapper mapper,
			IOptions<AppSettings> appSettings, IEmailService emailService)
		{
			_userService = userService ?? throw new ArgumentNullException(nameof(userService));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
			_appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
		}

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
				Id = user.Id,
				Username = user.UserName,
				Token = tokenString
			});
		}

		[AllowAnonymous]
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] UserRegister model)
		{
			// map model to entity
			var user = _mapper.Map<User>(model);

			try
			{
				// create user
				var result = await _userService.CreateAsync(user, model.Password);
			}
			catch (Exception ex)
			{
				// return error message if there was an exception
				return BadRequest(new {message = ex.Message});
			}

			return Ok();
		}

		[Authorize(Roles = "admin")]
		[HttpGet]
		public IActionResult GetAll()
		{
			var users = _userService.GetAllAsync();
			var model = _mapper.Map<IList<UserModel>>(users);
			return Ok(model);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var user = _userService.GetByIdAsync(id);
			var model = _mapper.Map<UserModel>(user);
			return Ok(model);
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, [FromBody] UserUpdate model)
		{
			// map model to entity and set id
			var user = _mapper.Map<User>(model);
			user.Id = id;

			try
			{
				// update user 
				_userService.UpdateAsync(user, model.Password);
				return Ok();
			}
			catch (AppException ex)
			{
				// return error message if there was an exception
				return BadRequest(new {message = ex.Message});
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _userService.DeleteAsync(id);
			return Ok();
		}


		[HttpGet]
		public async Task<IActionResult> Index() => Ok("U r sign in");

		
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task ForgotPassword(ForgotPasswordModel model)
		{
			var user = _userService.GetByEmailAsync(model.Email);
			var code = await _userService.ForgotPassword(model);
			var callbackUrl = Url.Action(action: "ResetPassword", controller: "Users",
				values: new {userId = user.Id, code = code},
				protocol: HttpContext.Request.Scheme);

			await _emailService.SendEmailAsync(model.Email, "Reset Password",
				$"To reset password follow link: <a href='{callbackUrl}'>link</a>");
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<ActionResult> ResetPassword([FromQuery]string code = null) => code == null ? (ActionResult) Forbid() : Ok(code);

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
		{
			await _userService.ResetPasswordAsync(model);

			return Ok();
		}
	}
}