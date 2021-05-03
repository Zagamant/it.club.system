using System.BLL.Models.PhotoManagement;
using System.DAL.Entities;
using AutoMapper;

namespace System.BLL.PhotoManagement
{
    public class ImageManagementMappingProfile : Profile
    {
        public ImageManagementMappingProfile()
        {
            CreateMap<Image, ImageModel>();
            CreateMap<ImageModel, Image>();
        }
    }
}