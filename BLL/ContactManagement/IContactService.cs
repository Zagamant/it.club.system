using System.BLL.Helpers;
using System.BLL.Models.ContactManagement;

namespace System.BLL.ContactManagement
{
    public interface IContactService : IBaseService<int, ContactModel, ContactModel, ContactModel>
    {
    }
}