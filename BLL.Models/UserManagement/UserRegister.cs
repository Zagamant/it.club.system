using System.ComponentModel.DataAnnotations;

namespace System.BLL.Models.UserManagement
{
    /// <summary>
    /// Represent authentication model for client registration
    /// </summary>
    public class UserRegister : UserModel
    {
	    
        /// <summary>
        /// Gets or sets user's password
        /// </summary>
        [Required]
        public string Password { get; set; }


    }
}
