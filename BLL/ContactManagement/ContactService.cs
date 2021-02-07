using System.BLL.Helpers;
using System.BLL.Models.ContactManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace System.BLL.ContactManagement
{
    public class ContactService : IContactService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ContactService(DataContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ContactModel>> GetAllAsync(string filter = "", string range = "",
            string sort = "")
        {
            return await _context.Contacts.Select(item => _mapper.Map<ContactModel>(item)).ToListAsync();
        }

        public async Task<ContactModel> GetAsync(int id)
        {
            var result = await _context.Contacts
                .SingleOrDefaultAsync(ev => ev.Id == id);

            if (result == null) throw new AppException($"Event with id: {id} not found.");

            var map = _mapper.Map<ContactModel>(result);
            return map;
        }

        public async Task<ContactModel> AddAsync(ContactModel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var result = await _context.Contacts
                .SingleOrDefaultAsync(ev => ev.Id == entity.Id);

            if (result != null) throw new AppException($"Event already exist.");

            var map = _mapper.Map<Contact>(entity);

            await _context.Contacts
                .AddAsync(map);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<ContactModel> UpdateAsync(int id, ContactModel newEntity)
        {
            if (_context.Costs.SingleOrDefaultAsync(evnt => evnt.Id == id) == null)
            {
                throw new AppException("Not found");
            }

            newEntity.Id = id;

            var map = _mapper.Map<Contact>(newEntity);

            _context.Contacts
                .Update(map);

            await _context.SaveChangesAsync();

            return newEntity;
        }

        public async Task DeleteAsync(int id, bool isDelete = false)
        {
            var result = await _context.Contacts
                .SingleOrDefaultAsync(contact => contact.Id == id);

            if (result == null) throw new AppException($"Costs with id: {id} not found.");

            _context.Contacts.Remove(result);

            await _context.SaveChangesAsync();
        }
    }
}