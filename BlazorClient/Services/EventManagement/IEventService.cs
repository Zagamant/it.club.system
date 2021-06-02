using System.BLL.Models.EventManagement;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.EventManagement
{
	public interface IEventService : IServiceBase<int, EventModel,EventModel,EventModel>
	{
	}
}
