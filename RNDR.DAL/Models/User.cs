using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace RNDR.DAL.Models
{
	/// <summary>
	/// Represent user in database.
	/// </summary>
	public class User : IdentityUser<int>
	{
		public virtual ICollection<Agreement> Agreements { get; set; }
	}
}