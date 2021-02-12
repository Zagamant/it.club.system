using System.BLL.Helpers;
using System.BLL.Models.ContactManagement;
using System.DAL;
using System.DAL.Entities;
using AutoMapper;

namespace System.BLL.ContactManagement
{
    public class ContactService : Repository<int, Contact, ContactModel, ContactModel, ContactModel>, IContactService
    {
        public ContactService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _table = _context.Contacts;
        }
    }
}