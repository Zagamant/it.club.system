using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace System.DAL.Models
{
	/// <summary>
	/// Represent user in database.
	/// </summary>
	public class User : IdentityUser<int>
	{
		public virtual InfoBase AboutUserInfo { get; set; }
		public virtual ICollection<Agreement> Agreements { get; set; } = new List<Agreement>();

	}
}