using System.BLL.Helpers;
using System.BLL.Models.ContactManagement;

namespace System.BLL.ContactManagement
{
    public interface IContactService : IRepository<int, ContactModel, ContactModel, ContactModel>
    {
    }
}