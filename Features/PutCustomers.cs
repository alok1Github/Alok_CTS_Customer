using AutoMapper;
using Customer.API.Models;
using Customer.DataAccess.BusinessObject;
using Customer.DataAccess.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.API.Features
{
    public interface IPutCustomer
    {
        Task<IEnumerable<CustomerModel>> Handler(CustomerModel request);
    }

    public class PutCustomers : IPutCustomer
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public PutCustomers(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public Task<IEnumerable<CustomerModel>> Handler(CustomerModel request)
        {
            var customer = this.mapper.Map<Customers>(request);

            return this.repository.UpdateCustomer(customer)
                          .ContinueWith(t =>
                                     this.mapper.Map<IEnumerable<CustomerModel>>(t.Result),
                                     TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}

