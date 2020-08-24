using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.DataAccess;
using Customer.DataAccess.BusinessObject;

namespace Customer.DataAccess.Data
{
    public class Repository : IRepository
    {
        public async Task<IEnumerable<Customers>> GetAllCustomer()
        {
            var container = CosmoDB.client.GetContainer("customers", "customers");
            var sql = "select * from c";
            var itrator = container.GetItemQueryIterator<Customers>(sql);
            return await itrator.ReadNextAsync();
        }

        //private async Task<IEnumerable<Customers>> GetAll()
        //{
        //    await new CustomerDataAccess().DataAccessSDK();


        //    ////return new List<Customers> {

        //    ////          new Customers { Name = "Test", Type = "Poc" },
        //    ////          new Customers { Name = "Test2", Type = "Poc2" },
        //    ////          new Customers { Name = "Test3", Type = "Poc3" }
        //    ////    };

        //}
    }
}
