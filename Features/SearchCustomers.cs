using AutoMapper;
using Customer.API.Models;
using Customer.DataAccess.BusinessObject;
using Customer.DataAccess.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.API.Features
{
    public class SearchCustomers : IGetCustomers
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public SearchCustomers(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public Task<IEnumerable<CustomerModel>> Handler(SerachModel searchRequest = null)
        {
            var serach = this.mapper.Map<Serach>(searchRequest);

            return this.repository.SearchCustomers(serach).ContinueWith(t =>
                                      this.mapper.Map<IEnumerable<CustomerModel>>(t.Result),
                                      TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}
