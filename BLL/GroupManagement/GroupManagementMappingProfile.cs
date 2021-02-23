using System.BLL.Models.GroupManagement;
using System.DAL.Entities;
using System.Linq;
using AutoMapper;

namespace System.BLL.GroupManagement
{
    public class GroupManagementMappingProfile : Profile
    {
        public GroupManagementMappingProfile()
        {
            CreateMap<Group, GroupModel>()
                .ForMember(gr => gr.UsersIds,
                    opt => opt.MapFrom(
                        g => g.Users.Select(user => user.Id).ToList()))
                ;
            CreateMap<GroupModel, Group>();
            CreateMap<Group, GroupRegisterModel>();
        }
    }
}