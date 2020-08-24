using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.DataAccess;
using Customer.DataAccess.BusinessObject;
using Microsoft.Azure.Cosmos;

namespace Customer.DataAccess.Data
{
    public class Repository : IRepository
    {
        public Task<IEnumerable<Customers>> GetAllCustomer() =>
            Task.Run(() => CosmoDB.container.GetItemLinqQueryable<Customers>().AsEnumerable());

        public async Task<Customers> CreateCustomer(Customers customer, PartitionKey id)
        {
            return await CosmoDB.container.CreateItemAsync(customer, id);
        }



    }
}
