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
        Task Handler(int customerId);
    }

    public class DeleteCustomers : IDeleteCustomer
    {
        private readonly IRepository repository;

        public DeleteCustomers(IRepository repository) => this.repository = repository;

        public Task Handler(int customerId)
        {
            return this.repository.DeleteCustomer(customerId);
        }
    }
}
