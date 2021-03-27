using System.BLL.Models.RoleManagement;
using System.DAL.Entities;
using AutoMapper;

namespace System.BLL.RoleManagement
{
    public class RoleManagementMappingProfile : Profile
    {
        public RoleManagementMappingProfile()
        {
            CreateMap<Role, RoleModel>();
        }
    }
}