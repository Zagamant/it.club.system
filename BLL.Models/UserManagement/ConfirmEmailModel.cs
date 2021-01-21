using System.ComponentModel.DataAnnotations;

namespace System.BLL.Models.UserManagement
{
	public class ConfirmEmailModel
	{
		[Required]
		public string Id { get; set; }
		
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		public string Code { get; set; }
	}
}
