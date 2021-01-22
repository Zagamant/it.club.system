using System.BLL.Models.EventManagement;
using System.DAL.Entities;
using AutoMapper;

namespace System.BLL.EventManagement
{
    public class EventManagementMappingProfile : Profile
    {
        public EventManagementMappingProfile()
        {
            CreateMap<Event, EventModel>();
            CreateMap<EventModel, Event>();
        }
        
    }
}