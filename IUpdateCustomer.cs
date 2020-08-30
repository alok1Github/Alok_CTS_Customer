using Customer.API.Models;
using System.Threading.Tasks;

namespace Customer.API
{
    public interface IUpdateCustomer
    {
        Task<CustomerModel> Handler(CustomerModel request);
    }
}
