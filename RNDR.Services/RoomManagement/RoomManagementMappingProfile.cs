using System.BLL.Models.RoomManagement;
using System.DAL.Entities;
using AutoMapper;

namespace System.BLL.RoomManagement
{
	public class RoomManagementMappingProfile : Profile
	{
		public RoomManagementMappingProfile()
		{
			CreateMap<Room, RoomModel>()
				.ReverseMap();

			CreateMap<Room, RoomSafeModel>()
				.ForMember(rm =>
						rm.ClubTitle,
					opt => opt.MapFrom(r => r.Club.Title));

		}
	}
}
