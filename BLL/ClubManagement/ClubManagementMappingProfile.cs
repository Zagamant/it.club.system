using System.BLL.Models.ClubManagement;
using System.DAL.Entities;
using AutoMapper;

namespace System.BLL.ClubManagement
{
	public class ClubManagementMappingProfile : Profile
	{
		public ClubManagementMappingProfile()
		{
			CreateMap<Club, ClubModel>();
			CreateMap<ClubModel, Club>();
		}
	}
}