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
            Task.Run(() => CosmoDB.Container.GetItemLinqQueryable<Customers>(allowSynchronousQueryExecution: true).AsEnumerable());

        public async Task<Customers> CreateCustomer(Customers customer)
        {
            try
            {
                return await CosmoDB.Container.CreateItemAsync(customer, new PartitionKey(customer.CustomerId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
