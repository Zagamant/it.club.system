using System.ComponentModel.DataAnnotations;

namespace System.BLL.Models.UserManagement
{
    /// <summary>
    /// Represent authentication model for client registration
    /// </summary>
    public class UserRegister
    {
	    /// <summary>
        /// Gets or sets user's username
        /// </summary>
        [Required]
        public string Username { get; set; }
	    /// <summary>
	    /// Gets or sets user's username
	    /// </summary>
	    public string Name { get; set; }
	    /// <summary>
	    /// Gets or sets user's username
	    /// </summary>
	    public string MiddleName{ get; set; }
	    /// <summary>
	    /// Gets or sets user's username
	    /// </summary>
	    public string Surname { get; set; }

        /// <summary>
        /// Gets or sets user's password
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets user's email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets user's phone number
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Gets or sets user's phone number
        /// </summary>
        public DateTime BirthDay { get; set; }

    }
}
