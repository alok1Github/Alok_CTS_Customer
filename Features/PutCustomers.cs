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
    public interface IPutCustomer
    {
        Task Handler(CustomerModel request);
    }

    public class PutCustomers : IPutCustomer
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public PutCustomers(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public Task Handler(CustomerModel request)
        {
            var customer = this.mapper.Map<Customers>(request);

            return this.repository.UpdateCustomer(customer);
        }
    }
}
