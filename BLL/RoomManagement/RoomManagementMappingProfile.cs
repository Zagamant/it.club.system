using System.BLL.Models.RoomManagement;
using System.DAL.Entities;
using AutoMapper;

namespace System.BLL.RoomManagement
{
	public class RoomManagementMappingProfile : Profile
	{
		public RoomManagementMappingProfile()
		{
			CreateMap<Room, RoomCreate>();
			CreateMap<RoomCreate, Room>();
		}
	}
}
