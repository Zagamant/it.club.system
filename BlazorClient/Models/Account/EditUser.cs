using System;
using System.BLL.Models.UserManagement;
using System.ComponentModel.DataAnnotations;

namespace BlazorClient.Models.Account
{
    public class EditUser
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string UserName { get; set; }

        [MinLength(6, ErrorMessage = "The Password field must be a minimum of 6 characters")]
        public string Password { get; set; }

        public EditUser() { }

        public EditUser(User user)
        {
            Name = user.Name;
            Surname = user.Surname;
            UserName = user.UserName;
        }

        public EditUser(UserModel user)
        {
            Name = user.Name;
            Surname = user.Surname;
            UserName = user.UserName;
            Password = String.Empty;
        }
    }
}