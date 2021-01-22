using System.BLL.Models.PaymentManagement;
using System.DAL.Entities;
using AutoMapper;

namespace System.BLL.PaymentManagement
{
    public class PaymentManagementMappingProfile : Profile
    {
        public PaymentManagementMappingProfile()
        {
            CreateMap<Payment, PaymentModel>()
                .ForMember(
                    model => model.UserId, 
                    opt => opt.MapFrom(c => c.User.Id));
            CreateMap<PaymentModel, Payment>();
        }
    }
}