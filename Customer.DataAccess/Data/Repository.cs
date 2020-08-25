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

        public async Task DeleteCustomer(int customerId)
        {
            var customers = CosmoDB.Container.GetItemLinqQueryable<Customers>(allowSynchronousQueryExecution: true)
                                             .Where(c => c.CustomerId == customerId)
                                             .ToList();

            foreach (var customer in customers)
            {
                await CosmoDB.Container.DeleteItemAsync<Customers>(customer.Id, new PartitionKey(customer.CustomerId));
            }
        }

        public async Task UpdateCustomer(Customers customer)
        {
            var customers = CosmoDB.Container.GetItemLinqQueryable<Customers>(allowSynchronousQueryExecution: true)
                                           .Where(c => c.CustomerId == customer.CustomerId)
                                           .ToList();
            foreach (var document in customers)
            {
                document.BankDetails = customer.BankDetails;
                document.Address = customer.Address;
                document.PersonalDetail = customer.PersonalDetail;
               await CosmoDB.Container.ReplaceItemAsync<Customers>(document, document.Id, new PartitionKey(document.CustomerId));                  
            }

        }
    }
}
