namespace RNDR.Services.Models.ModelManagement
{
    /// <summary>
    /// Represent authentication model for client update information
    /// </summary>
    public class UserUpdate
    {
        /// <summary>
        /// Gets or sets user's firstname
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets user's lastname
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets user's username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets user's password
        /// </summary>
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
