using Customer.DataAccess.Data;
using System.Threading.Tasks;

namespace Customer.API.Features
{
    public interface IDeleteCustomer
    {
        Task Handler(string customerId);
    }

    public class DeleteCustomers : IDeleteCustomer
    {
        private readonly IRepository repository;
        public DeleteCustomers(IRepository repository) => this.repository = repository;
        public Task Handler(string customerId) => this.repository.DeleteCustomer(customerId);
    }
}
