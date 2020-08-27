using Customer.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.API
{
    public interface IGetCustomers
    {
        Task<IEnumerable<CustomerModel>> Handler(SerachModel searchRequest = null);
    }
}
