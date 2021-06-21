using System.ComponentModel.DataAnnotations;
using System;

namespace BlazorClient.Models.Account
{
    public class AddUser
    {
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public DateTime BirthDay { get; set; }
 
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "The Password field must be a minimum of 6 characters")]
        public string Password { get; set; }
    }
}