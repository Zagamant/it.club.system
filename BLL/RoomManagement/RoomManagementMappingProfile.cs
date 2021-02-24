using System.BLL.Models.RoomManagement;
using System.DAL.Entities;
using System.Linq;
using AutoMapper;

namespace System.BLL.RoomManagement
{
	public class RoomManagementMappingProfile : Profile
	{
		public RoomManagementMappingProfile()
		{
			CreateMap<Room, RoomCreateModel>();
			CreateMap<RoomCreateModel, Room>();
			CreateMap<Room, RoomModel>()
				.ForMember(r => r.GroupIds,
					opt 
						=> opt.MapFrom(rm => rm.Groups.Select(gr => gr.Id).ToList()));
			CreateMap<RoomModel, Room>();
		}
	}
}
