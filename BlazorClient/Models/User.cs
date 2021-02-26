using System;

namespace BlazorClient.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDay { get; set; }
        public string AdditionalInfo { get; set; }
        public string Token { get; set; }
    }
}