using System.BLL.Models.Helpers;

namespace System.BLL.Models.ContactManagement
{
    public class ContactModel : BaseModel
    {

        public string Name { get; set; }

        public string ContactType { get; set; }

        public string ContactAsIs { get; set; }
    }
}