using AutoMapper;
using EPPaymentWebApp.Models;
using EPPCIDAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPPaymentWebApp.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PaymentViewModelDTO,PaymentViewModel >();
        }
    }
}
