using System.BLL.Models.ContactManagement;
using System.Net.Http;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.ContactManagement
{
    public class ContactService : Repository<int, ContactModel, ContactModel, ContactModel>, IContactService
    {
        public ContactService(HttpClient http) : base(http)
        {
        }
    }
}