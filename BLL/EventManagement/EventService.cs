using System.BLL.Helpers;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace System.BLL.EventManagement
{
	public class EventService : IEventService
	{
		private readonly DataContext _context;

		public EventService(DataContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public async Task<IEnumerable<Event>> GetAllAsync()
		{
			var result = await _context.Events
				.AsNoTracking()
				.ToListAsync();

			return result;
		}

		public async Task<Event> GetAsync(int id)
		{
			var result = await _context.Events
				.AsNoTracking()
				.SingleOrDefaultAsync(ev => ev.Id == id);

			if(result == null) throw new AppException($"Event with id: {id} not found.");

			return result;
		}

		public async Task AddAsync(Event entity)
		{
			if(entity == null) throw new ArgumentNullException(nameof(entity));
			Event result = null;
			if (entity.Id != 0)
			{
				result = await _context.Events
					.AsNoTracking()
					.SingleOrDefaultAsync(ev => ev.Id == entity.Id);

				if (result != null) throw new AppException($"Event already exist.");
			}

			await _context.Events
				.AddAsync(entity);

			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Event dbEntity, Event newEntity)
		{
			if (newEntity == null) throw new ArgumentNullException(nameof(newEntity));
			if (dbEntity == null) throw new ArgumentNullException(nameof(dbEntity));


			var result = await _context.Events
				.SingleOrDefaultAsync(ev => ev.Id == dbEntity.Id);

			if (result == null) throw new AppException($"Event not found.");

			newEntity.Id = dbEntity.Id;
			_context.Events
				.Update(newEntity);

			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Event entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			var result = await _context.Events
				.SingleOrDefaultAsync(cos => cos.Id == entity.Id);

			if (result == null) throw new AppException($"Costs with id: {entity.Id} not found.");

			_context.Events.Remove(entity);

			await _context.SaveChangesAsync();
		}
	}
}
