using System.DAL.Entities;

namespace System.BLL.Models.UserManagement
{
    /// <summary>
    /// Represent authentication model for client update information
    /// </summary>
    public class UserUpdate : User
    {
	    /// <summary>
        /// Gets or sets user's password
        /// </summary>
        public string Password { get; set; }

    }
}
