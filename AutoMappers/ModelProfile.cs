using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.API.Models;
using Customer.DataAccess.BusinessObject;
using AutoMapper;

namespace Customer.API.AutoMappers
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            CreateMap<Customers, CustomerModel>().ReverseMap();
            CreateMap<BankDetails, BankDetailsModel>().ReverseMap();
            CreateMap<PersonalDetails, PersonalDetailsModel>().ReverseMap();
            CreateMap<Address, AddressModel>().ReverseMap();
            CreateMap<Serach, SerachModel>().ReverseMap();
        }
    }
}
