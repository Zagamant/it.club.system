using System.BLL.Helpers;
using System.BLL.Models.EventManagement;
using System.DAL.Entities;

namespace System.BLL.EventManagement
{
	public interface IEventService : IRepository<int, EventModel>
	{
	}
}
