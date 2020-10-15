using Microsoft.AspNetCore.Identity;

namespace System.DAL.Models
{
	public class Role : IdentityRole<int>
	{
		public Role() : base() 
		{ }

		public Role(string roleName) : base(roleName)
		{ }
	}
}
