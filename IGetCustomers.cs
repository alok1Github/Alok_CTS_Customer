using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.API.Models;
using Customer.DataAccess.BusinessObject;

namespace Customer.API
{
    public interface IGetCustomers
    {
        Task<IEnumerable<CustomerModel>> Handler(SerachModel searchRequest = null);
    }
}
