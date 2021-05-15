using System.BLL.Models.Helpers;
using System.BLL.Models.RoleManagement;
using System.Collections.Generic;
using System.DAL.Entities;
using System.Threading.Tasks;

namespace System.BLL.Models.UserManagement
{
    /// <summary>
    /// Represent user model for server
    /// </summary>
    public class UserModel : BaseModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDay { get; set; }
        public string AdditionalInfo { get; set; }
        public virtual IEnumerable<Image> Photos { get; set; } = new List<Image>();
        public virtual IList<string> Roles { get; set; } = new List<string>();
        public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
        public virtual ICollection<Group> Groups { get; set; } = new List<Group>();  
        public virtual ICollection<Course> CoursesPassed { get; set; } = new List<Course>();

        public string Country { get; set; }
        public string City { get; set; }
        public string AddressLine { get; set; }

        public string NewPassword { get; set; }
    }
}
