using System.BLL.Models.ClubManagement;
using System.DAL.Entities;
using AutoMapper;

namespace System.BLL.ClubManagement
{
	public class ClubManagementMappingProfile : Profile
	{
		public ClubManagementMappingProfile()
		{
			CreateMap<Club, ClubModel>()
				.ForMember(dest => 
						dest.City,
					opt => opt.MapFrom(src => src.Address.City))
				.ForMember(dest => 
						dest.AddressLine,
					opt => opt.MapFrom(src => src.Address.AddressLine))
				.ForMember(dest => 
						dest.Country,
					opt => opt.MapFrom(src => src.Address.Country));
            
            
			CreateMap<ClubModel, Club>()
				.ForMember(dest => 
						dest.Address,
					opt => opt.MapFrom(src => new Address
					{
						Id = src.Id,
						AddressLine = src.AddressLine,
						City = src.City,
						Country = src.Country
					}));
		}
	}
}