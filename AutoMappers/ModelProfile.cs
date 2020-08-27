using AutoMapper;
using Customer.API.Models;
using Customer.DataAccess.BusinessObject;

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
