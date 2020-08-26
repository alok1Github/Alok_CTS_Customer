using AutoMapper;
using Customer.API.Models;
using Customer.DataAccess.BusinessObject;
using Customer.DataAccess.Data;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.API.Features
{
    public interface IPostCustomer
    {
        Task<CustomerModel> Handler(CustomerModel request);
    }

    public class PostCustomer : IPostCustomer
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

            customer.Id = Guid.NewGuid().ToString();

            return this.repository.CreateCustomer(customer)
                                   .ContinueWith(t => this.mapper.Map<CustomerModel>(t.Result),
                                    TaskContinuationOptions.OnlyOnRanToCompletion);

        }
    }
}
