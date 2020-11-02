using System.BLL.Helpers;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Models;
using System.Threading.Tasks;

namespace System.BLL.EventManagement
{
	public class EventService : IRepository<Event>
	{
		private readonly DataContext _context;

		public EventService(DataContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public async Task<IEnumerable<Event>> GetAll()
		{
			throw new NotImplementedException();
		}

		public async Task<Event> Get(int id)
		{
			throw new NotImplementedException();
		}

		public async Task Add(Event entity)
		{
			throw new NotImplementedException();
		}

		public async Task Update(Event dbEntity, Event entity)
		{
			throw new NotImplementedException();
		}

		public async Task Delete(Event entity)
		{
			throw new NotImplementedException();
		}
	}
}
