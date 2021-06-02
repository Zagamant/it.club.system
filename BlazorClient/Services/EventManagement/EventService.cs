using System.BLL.Models.EventManagement;
using System.Net.Http;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.EventManagement
{
    public class EventService : ServiceBase<int, EventModel, EventModel, EventModel>,
        IEventService
    {
        public EventService(HttpClient http) : base(http)
        {
        }
    }
}