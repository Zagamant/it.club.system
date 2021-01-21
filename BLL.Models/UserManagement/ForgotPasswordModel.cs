using System.ComponentModel.DataAnnotations;

namespace System.BLL.Models.UserManagement
{
	public class ForgotPasswordModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
