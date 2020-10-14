using AutoMapper;
using RNDR.DAL.Models;
using RNDR.Services.Models.ModelManagement;

namespace RNDR.Services.UserManagement
{
    public class UserManagementMappingProfile : Profile
    {
        public UserManagementMappingProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserRegister, User>();
            CreateMap<UserUpdate, User>();
            CreateMap<UserAuthenticate, User>();
            CreateMap<UserModel, User>();
        }
    }
}
