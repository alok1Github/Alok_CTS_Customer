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
    public interface IDeleteCustomer
    {
        Task<bool> Handler(int customerId);
    }

    public class DeleteCustomer : IDeleteCustomer
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public DeleteCustomer(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<bool> Handler(int customerId)
        {
            await this.repository.DeleteCustomer(customerId);
            return true;
        }


    }
}
