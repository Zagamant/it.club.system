using System.ComponentModel.DataAnnotations;

namespace RNDR.Services.Models.ModelManagement
{
    /// <summary>
    /// Represent authentication model for client authenticate
    /// </summary>
    public class UserAuthenticate
    {
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
    }
}
