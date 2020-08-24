using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.API.Models;
using Customer.DataAccess.BusinessObject;
using AutoMapper;

namespace Customer.API.AutoMappers
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customers, CustomerModel>();
            CreateMap<Address, AddressModel>();
        }
    }
}
