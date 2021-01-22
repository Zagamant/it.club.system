using System.BLL.Models.PaymentManagement;
using System.BLL.Models.PhotoManagement;
using System.DAL.Entities;
using AutoMapper;

namespace System.BLL.PhotoManagement
{
    public class PhotoManagementMappingProfile : Profile
    {
        public PhotoManagementMappingProfile()
        {
            CreateMap<Photo, PhotoModel>();
            CreateMap<PaymentModel, Payment>();
        }
    }
}