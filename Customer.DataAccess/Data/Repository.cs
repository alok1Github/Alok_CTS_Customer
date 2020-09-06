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
        public static Container mainContainer;
        public static Container replicaContainer;
        public static Container leaseContainer;

        public Repository()
        {
            mainContainer = CosmoDB.MainContainer;
            replicaContainer = CosmoDB.ReplicaContainer;
            leaseContainer = CosmoDB.LeaseContainer;
        }

        public Task<IEnumerable<Customers>> GetAllCustomers() =>
            Task.Run(() => mainContainer.GetItemLinqQueryable<Customers>(allowSynchronousQueryExecution: true).AsEnumerable());

        public async Task<Customers> CreateCustomer(Customers customer)
        {
            customer.Id = Guid.NewGuid().ToString();
            customer.customerId = customer.Id;

            return await mainContainer.UpsertItemAsync(customer, new PartitionKey(customer.customerId));
        }


        public async Task<Customers> DeleteCustomer(Customers customer)
        {
            var document = await GetCustomers(c => c.Id == customer.customerId, mainContainer);

            if (document.Any())
            {
                document[0].TTL = customer.TTL;
                document[0].Id = customer.customerId;

                var task = await mainContainer.ReplaceItemAsync(document[0], document[0].Id, new PartitionKey(document[0].customerId));

                return task.Resource;
            }

            return null;
        }


        public async Task<Customers> UpdateCustomer(Customers customer)
        {
            var document = await GetCustomers(c => c.Id == customer.customerId, mainContainer);

            if (document.Any())
            {

                document[0].Id = customer.customerId;
                document[0].BankDetails = customer.BankDetails;
                document[0].Address = customer.Address;
                document[0].PersonalDetail = customer.PersonalDetail;

                var task = await mainContainer.ReplaceItemAsync(document[0], document[0].Id, new PartitionKey(document[0].customerId));

                return task.Resource;
            }

            return null;
        }

        public async Task<IEnumerable<Customers>> SearchCustomers(Serach serachTerm)
        {
            if (serachTerm == null)
            {
                return null;
            }
            if (serachTerm.DOB != null && string.IsNullOrEmpty(serachTerm.ZipCode))
            {
                return await GetCustomers(c => c.PersonalDetail.DOB == serachTerm.DOB, replicaContainer);
            }

            else if (serachTerm.DOB == null && !string.IsNullOrEmpty(serachTerm.ZipCode))
            {
                return await GetCustomers(c => c.Address.ZipCode == serachTerm.ZipCode, replicaContainer);
            }

            else
            {
                return await GetCustomers(c => c.PersonalDetail.DOB == serachTerm.DOB && c.Address.ZipCode == serachTerm.ZipCode, replicaContainer);
            }
        }

        private static async Task<List<Customers>> GetCustomers(Expression<Func<Customers, bool>> whereClause, Container container)
        {
            return await Task.Run(() => container.GetItemLinqQueryable<Customers>(allowSynchronousQueryExecution: true).Where(whereClause).ToList());
        }
    }
}
