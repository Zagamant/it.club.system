using System.BLL.Helpers;
using System.BLL.Models.ContactManagement;

namespace BlazorClient.Services.ContactManagement
{
    public interface IContactService : Helpers.IServiceBase<int, ContactModel, ContactModel, ContactModel>
    {
    }
}