using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.DataAccess.BusinessObject;

namespace Customer.API.Models
{
    public class CustomerModel
    {
        public string Name { get; set; }
        public AddressModel Address { get; set; }

        public CustomerModel()
        {
        }
    }
}
