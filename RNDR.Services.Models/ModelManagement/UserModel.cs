namespace RNDR.Services.Models.ModelManagement
{
    /// <summary>
    /// Represent user model for server
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Gets or sets user's id
        /// </summary>
        public int Id { get; set; }

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
