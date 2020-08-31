using AutoMapper;
using Customer.API.Models;
using Customer.DataAccess.BusinessObject;
using Customer.DataAccess.Data;
using System.Threading.Tasks;

namespace Customer.API.Features
{
    public interface IDeleteCustomer
    {
        Task<CustomerModel> Handler(CustomerModel request);
    }

    public class DeleteCustomers : IDeleteCustomer
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public DeleteCustomers(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public Task<CustomerModel> Handler(CustomerModel request)
        {
            var customer = this.mapper.Map<Customers>(request);

            return this.repository.DeleteCustomer(customer)
                          .ContinueWith(t =>
                                     this.mapper.Map<CustomerModel>(t.Result),
                                     TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}
