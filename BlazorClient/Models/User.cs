using System.Collections;
using System.Collections.Generic;
using System.DAL.Entities;

namespace BlazorClient.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public bool IsDeleting { get; set; }
        public IEnumerable<Image> Images { get; set; }
    }
}