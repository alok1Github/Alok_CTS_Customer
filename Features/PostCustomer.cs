using AutoMapper;
using Customer.API.Models;
using Customer.DataAccess.BusinessObject;
using Customer.DataAccess.Data;
using System;
using System.Threading.Tasks;

namespace Customer.API.Features
{

    public class PostCustomer : IUpdateCustomer
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public PostCustomer(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public Task<CustomerModel> Handler(CustomerModel request)
        {
            var customer = this.mapper.Map<Customers>(request);

            return this.repository.CreateCustomer(customer)
                                   .ContinueWith(t => this.mapper.Map<CustomerModel>(t.Result),
                                    TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}
