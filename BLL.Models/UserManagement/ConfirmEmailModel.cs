using System.BLL.Models.Helpers;
using System.ComponentModel.DataAnnotations;

namespace System.BLL.Models.UserManagement
{
	public class ConfirmEmailModel : BaseModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		public string Code { get; set; }
	}
}
