using AutoMapper;
using RNDR.DAL.Models;

namespace RNDR.Services.Models.ModelManagement
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
