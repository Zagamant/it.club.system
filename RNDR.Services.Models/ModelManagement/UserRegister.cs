using System.ComponentModel.DataAnnotations;

namespace RNDR.Services.Models.ModelManagement
{
    /// <summary>
    /// Represent authentication model for client registration
    /// </summary>
    public class UserRegister
    {
        /// <summary>
        /// Gets or sets user's firstname
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets user's lastname
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets user's username
        /// </summary>
        [Required]
        public string Username { get; set; }

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
        public string Phone { get; set; }
    }
}
