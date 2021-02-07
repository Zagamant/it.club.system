using System.BLL.Helpers;
using System.BLL.Models.EventManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace System.BLL.EventManagement
{
    public class EventService : IEventService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EventService(DataContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<EventModel>> GetAllAsync(string filter = "", string range = "",
            string sort = "")
        {
            var result = await _context.Events
                .AsNoTracking()
                .ToListAsync();

            return result.Select(item => _mapper.Map<EventModel>(item)).ToList();
        }

        public async Task<EventModel> GetAsync(int id)
        {
            var result = await _context.Events
                .AsNoTracking()
                .SingleOrDefaultAsync(ev => ev.Id == id);

            if (result == null) throw new AppException($"Event with id: {id} not found.");

            var map = _mapper.Map<EventModel>(result);
            return map;
        }

        public async Task<EventModel> AddAsync(EventModel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var result = await _context.Events
                .SingleOrDefaultAsync(ev => ev.Id == entity.Id);

            if (result != null) throw new AppException($"Event already exist.");

            var map = _mapper.Map<Event>(entity);

            await _context.Events
                .AddAsync(map);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<EventModel> UpdateAsync(EventModel dbEntity, EventModel newEntity)
        {
            if (newEntity == null) throw new ArgumentNullException(nameof(newEntity));
            if (dbEntity == null) throw new ArgumentNullException(nameof(dbEntity));

            if (await _context.Events
                .SingleOrDefaultAsync(ev => ev.Id == dbEntity.Id) == null)
                throw new AppException($"Event not found.");

            newEntity.Id = dbEntity.Id;

            var result = _mapper.Map<Event>(newEntity);
            _context.Events
                .Update(result);

            await _context.SaveChangesAsync();

            return newEntity;
        }

        public async Task<EventModel> UpdateAsync(int id, EventModel newEntity)
        {
            if (!_context.Events.Any(evnt => evnt.Id == id))
            {
                throw new AppException("Not found");
            }

            newEntity.Id = id;

            var map = _mapper.Map<Event>(newEntity);

            _context.Events
                .Update(map);

            await _context.SaveChangesAsync();

            return newEntity;
        }


        public async Task DeleteAsync(EventModel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var result = await _context.Events
                .SingleOrDefaultAsync(cos => cos.Id == entity.Id);

            if (result == null) throw new AppException($"Costs with id: {entity.Id} not found.");

            _context.Events.Remove(result);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, bool isDelete = false)
        {
            var result = await _context.Events
                .SingleOrDefaultAsync(Event => Event.Id == id);

            if (result == null) throw new AppException($"Costs with id: {id} not found.");

            _context.Events.Remove(result);

            await _context.SaveChangesAsync();
        }
    }
}