﻿using Customer.DataAccess.BusinessObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.DataAccess.Data
{
    public interface IRepository
    {
        Task<IEnumerable<Customers>> GetAllCustomers();
        Task<Customers> CreateCustomer(Customers customer);
        Task DeleteCustomer(int customerId);
        Task<IEnumerable<Customers>> UpdateCustomer(Customers customer);
        Task<IEnumerable<Customers>> SearchCustomers(Serach customer);
    }
}
