using Customer.DataAccess.BusinessObject;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.DataAccess.Data
{
    public interface IRepository
    {

        Task<IEnumerable<Customers>> GetAllCustomer();
        Task<Customers> CreateCustomer(Customers customer);


        // Task<IEnumerable<TBusinessObject>> GetAll<TBusinessObject, Tmodel>(TBusinessObject document) where TBusinessObject : BusinessObjectBase, new();
        //   void Delete(BusinessObjectBase entity);

        //  Task<IEnumerable<TBusinessObject>> FindCustomer<TBusinessObject, Tmodel>(TBusinessObject document) where TBusinessObject : BusinessObjectBase;
    }
}
