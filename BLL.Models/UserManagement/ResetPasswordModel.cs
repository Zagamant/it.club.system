using System.ComponentModel.DataAnnotations;

namespace System.BLL.Models.UserManagement
{
	public class ResetPasswordModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "Password must contains at least 6 symbols", MinimumLength = 6)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "Passwords are not equals")]
		public string ConfirmPassword { get; set; }

		public string Code { get; set; }
	}
}
