using System.BLL.Helpers;
using System.BLL.Models.ContactManagement;
using System.DAL;
using System.DAL.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace System.BLL.ContactManagement
{
    public class ContactService : BaseService<int, Contact, ContactModel, ContactModel, ContactModel>, IContactService
    {
        public ContactService(DataContext context, IMapper mapper, ILogger<ContactService> logger) : base(context, mapper, logger)
        {
            _table = _context.Contacts;
        }
    }
}