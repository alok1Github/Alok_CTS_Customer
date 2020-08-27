using Customer.DataAccess.BusinessObject;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Customer.DataAccess.Data
{
    public class Repository : IRepository
    {
        public Task<IEnumerable<Customers>> GetAllCustomers() =>
            Task.Run(() => CosmoDB.Container.GetItemLinqQueryable<Customers>(allowSynchronousQueryExecution: true).AsEnumerable());

        public async Task<Customers> CreateCustomer(Customers customer) =>
             await CosmoDB.Container.CreateItemAsync(customer, new PartitionKey(customer.CustomerId));


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

        public async Task<IEnumerable<Customers>> UpdateCustomer(Customers customer)
        {
            var customers = CosmoDB.Container.GetItemLinqQueryable<Customers>(allowSynchronousQueryExecution: true)
                                           .Where(c => c.CustomerId == customer.CustomerId)
                                           .ToList();

            var updatedCustomer = new List<Customers>();

            foreach (var document in customers)
            {
                document.BankDetails = customer.BankDetails;
                document.Address = customer.Address;
                document.PersonalDetail = customer.PersonalDetail;
                var task = await CosmoDB.Container.ReplaceItemAsync(document, document.Id, new PartitionKey(document.CustomerId));

                updatedCustomer.Add(task.Resource);
            }

            return updatedCustomer;

        }

        public async Task<IEnumerable<Customers>> SearchCustomers(Serach serachTerm)
        {
            if (serachTerm == null)
            {
                return null;
            }
            if (serachTerm.DOB != null && string.IsNullOrEmpty(serachTerm.ZipCode))
            {
                return await SearchCustomers(c => c.PersonalDetail.DOB == serachTerm.DOB);
            }

            else if (serachTerm.DOB == null && !string.IsNullOrEmpty(serachTerm.ZipCode))
            {
                return await SearchCustomers(c => c.Address.ZipCode == serachTerm.ZipCode);
            }

            else
            {
                return await SearchCustomers(c => c.Address.ZipCode == serachTerm.ZipCode && c.PersonalDetail.DOB == serachTerm.DOB);
            }
        }

        private static Task<List<Customers>> SearchCustomers(Expression<Func<Customers, bool>> whereClause)
        {
            return Task.Run(() => CosmoDB.Container.GetItemLinqQueryable<Customers>(allowSynchronousQueryExecution: true)
                                           .Where(whereClause)
                                           .ToList());
        }
    }
}
