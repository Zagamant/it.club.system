using System.BLL.Models.GroupManagement;
using System.DAL.Entities;
using AutoMapper;

namespace System.BLL.GroupManagement
{
	public class GroupManagementMappingProfile : Profile
	{
		public GroupManagementMappingProfile()
		{
			CreateMap<Group, GroupModel>();
			CreateMap<GroupModel, Group>();
			CreateMap<Group, GroupRegisterModel>();
		}
	}
}
