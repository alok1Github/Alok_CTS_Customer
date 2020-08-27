﻿using AutoMapper;
using Customer.API.Models;
using Customer.DataAccess.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.API.Features
{
    public class GetCustomers : IGetCustomers
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public GetCustomers(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public Task<IEnumerable<CustomerModel>> Handler(SerachModel searchRequest = null) =>
             this.repository.GetAllCustomers().ContinueWith(t =>
             this.mapper.Map<IEnumerable<CustomerModel>>(t.Result),
             TaskContinuationOptions.OnlyOnRanToCompletion);

    }
}
