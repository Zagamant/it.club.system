using System.BLL.Helpers;
using System.BLL.Models.ContactManagement;

namespace BlazorClient.Services.ContactManagement
{
    public interface IContactService : Helpers.IRepository<int, ContactModel, ContactModel, ContactModel>
    {
    }
}