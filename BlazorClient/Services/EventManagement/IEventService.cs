using System.BLL.Models.EventManagement;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.EventManagement
{
	public interface IEventService : IRepository<int, EventModel,EventModel,EventModel>
	{
	}
}
