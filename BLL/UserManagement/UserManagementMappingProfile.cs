using System.BLL.Models.UserManagement;
using System.DAL.Entities;
using AutoMapper;

namespace System.BLL.UserManagement
{
    public class UserManagementMappingProfile : Profile
    {
        public UserManagementMappingProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(dest => 
                    dest.City,
                    opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => 
                        dest.AddressLine,
                    opt => opt.MapFrom(src => src.Address.AddressLine))
                .ForMember(dest => 
                    dest.Country,
                opt => opt.MapFrom(src => src.Address.Country));
            
            
            CreateMap<UserModel, User>()
                .ForMember(dest => 
                        dest.Address,
                    opt => opt.MapFrom(src => new Address
                    {
                        Id = src.Id,
                        AddressLine = src.AddressLine,
                        City = src.City,
                        Country = src.Country
                    }));
            CreateMap<UserRegister, User>();
            CreateMap<UserUpdate, User>();
            CreateMap<UserAuthenticate, User>();
        }
    }
}
