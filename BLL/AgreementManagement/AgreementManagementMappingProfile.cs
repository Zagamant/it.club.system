using System.BLL.Models.AgreementManagement;
using System.DAL.Entities;
using AutoMapper;

namespace System.BLL.AgreementManagement
{
	public class AgreementManagementMappingProfile: Profile
	{
		public AgreementManagementMappingProfile()
		{
			CreateMap<Agreement, AgreementModel>();
		}
	}
}
