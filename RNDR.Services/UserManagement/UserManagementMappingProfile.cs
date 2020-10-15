using System.BLL.Models.UserManagement;
using System.DAL.Models;
using AutoMapper;

namespace System.BLL.UserManagement
{
    public class UserManagementMappingProfile : Profile
    {
        public UserManagementMappingProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
            CreateMap<UserRegister, User>();
            CreateMap<UserUpdate, User>();
            CreateMap<UserAuthenticate, User>();
        }
    }
}
