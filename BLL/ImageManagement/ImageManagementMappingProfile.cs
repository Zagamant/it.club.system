using System.BLL.Models.ImageManagement;
using System.DAL.Entities;
using AutoMapper;

namespace System.BLL.ImageManagement
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