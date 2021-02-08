﻿using System.BLL.Helpers;
using System.BLL.Models.EventManagement;
using System.DAL;
using System.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace System.BLL.EventManagement
{
    public class EventService : Repository<int, Event, EventModel,EventModel,EventModel>, IEventService
    {
        public EventService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _table = _context.Events;
        }
        
        public override async Task<EventModel> AddAsync(EventModel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var result = await _context.Events
                .SingleOrDefaultAsync(ev => ev.Id == entity.Id);

            if (result != null) throw new AppException($"Event already exist.");

            var map = _mapper.Map<Event>(entity);

            map.Club = await _context.Clubs.SingleOrDefaultAsync(e => e.Id == map.ClubId);
            
            await _context.Events
                .AddAsync(map);

            await _context.SaveChangesAsync();

            return entity;
        }

        public override async Task<EventModel> UpdateAsync(int id, EventModel newEntity)
        {
            if (!_context.Events.Any(evnt => evnt.Id == id))
            {
                throw new AppException("Not found");
            }

            newEntity.Id = id;

            var map = _mapper.Map<Event>(newEntity);

            map.Club = await _context.Clubs.SingleOrDefaultAsync(e => e.Id == map.ClubId);

            _context.Events
                .Update(map);

            await _context.SaveChangesAsync();

            return newEntity;
        }
    }
}