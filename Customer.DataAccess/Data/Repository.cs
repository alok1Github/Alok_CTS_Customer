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
             await CosmoDB.Container.UpsertItemAsync(customer, new PartitionKey(customer.Id));


        public async Task DeleteCustomer(string customerId)
        {
            var customer = await GetCustomers(c => c.Id == customerId);

            await CosmoDB.Container.DeleteItemAsync<Customers>(customer[0].Id, new PartitionKey(customer[0].Id));
        }


        public async Task<Customers> UpdateCustomer(Customers customer)
        {
            var document = await GetCustomers(c => c.Id == customer.Id);

            document[0].BankDetails = customer.BankDetails;
            document[0].Address = customer.Address;
            document[0].PersonalDetail = customer.PersonalDetail;

            var task = await CosmoDB.Container.ReplaceItemAsync(document[0], document[0].Id, new PartitionKey(document[0].Id));

            return task.Resource;
        }

        public async Task<IEnumerable<Customers>> SearchCustomers(Serach serachTerm)
        {
            if (serachTerm == null)
            {
                return null;
            }
            if (serachTerm.DOB != null && string.IsNullOrEmpty(serachTerm.ZipCode))
            {
                return await GetCustomers(c => c.PersonalDetail.DOB == serachTerm.DOB);
            }

            else if (serachTerm.DOB == null && !string.IsNullOrEmpty(serachTerm.ZipCode))
            {
                return await GetCustomers(c => c.Address.ZipCode == serachTerm.ZipCode);
            }

            else
            {
                return await GetCustomers(c => c.Address.ZipCode == serachTerm.ZipCode && c.PersonalDetail.DOB == serachTerm.DOB);
            }
        }

        private static Task<List<Customers>> GetCustomers(Expression<Func<Customers, bool>> whereClause)
        {
            var task = Task.Run(() => CosmoDB.Container.GetItemLinqQueryable<Customers>(allowSynchronousQueryExecution: true)
                                           .Where(whereClause)
                                           .ToList());

            return task.ContinueWith(t =>
            {
                if (!t.Result.Any()) throw new InvalidOperationException("No customer record found in database for this id");
                return t.Result;
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}
