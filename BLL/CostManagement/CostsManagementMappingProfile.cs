using System.BLL.Models.CostsManagement;
using System.DAL.Entities;
using AutoMapper;

namespace System.BLL.CostManagement
{
    public class CostsManagementMappingProfile : Profile
    {
        public CostsManagementMappingProfile()
        {
            CreateMap<Costs, CostsModel>();
            CreateMap<CostsModel, Costs>();
        }
        
    }
}